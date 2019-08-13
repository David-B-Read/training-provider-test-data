
namespace TrainingProviderTestData.Application.Importers
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using CsvHelper;
    using CsvHelper.TypeConversion;
    using Interfaces;
    using Microsoft.Extensions.Logging;
    using TrainingProviderTestData.Application.Models;

    public class CompaniesHouseDataImporter : ICompaniesHouseDataImporter
    {
        private readonly ITestDataRepository _testDataRepository;
        private readonly ILogger<CompaniesHouseDataImporter> _logger;
        private readonly ICompaniesHouseApiClient _client;

        public CompaniesHouseDataImporter(ITestDataRepository testDataRepository, ILogger<CompaniesHouseDataImporter> logger, ICompaniesHouseApiClient client)
        {
            _testDataRepository = testDataRepository;
            _logger = logger;
            _client = client;
        }

        public async Task<bool> ImportCompaniesHouseData(StreamReader streamReader)
        {
            var entries = new List<CompaniesHouseDataEntry>();

            using (var csvReader = new CsvReader(streamReader))
            {
                csvReader.Configuration.CultureInfo = CultureInfo.CreateSpecificCulture("en-GB");
                while (csvReader.Read())
                {
                    try
                    {
                        var record = csvReader.GetRecord<CompaniesHouseDataEntry>();

                        if (record != null)
                        { 
                            entries.Add(record);
                        }
                    }
                    catch (TypeConverterException typeConverterException)
                    {
                        _logger.LogError("Unable to load companies house data due to type conversion error", typeConverterException);
                        return await Task.FromResult(false);
                    }
                    catch (HeaderValidationException headerValidationException)
                    {
                        _logger.LogError("Unable to load companies house data due to header validation error", headerValidationException);
                        return await Task.FromResult(false);
                    }
                }
            }

            if (entries.Any())
            {
                await _testDataRepository.DeleteCompaniesHouseData();

                foreach (var entry in entries)
                {
                   bool success = await _testDataRepository.ImportCompaniesHouseData(entry);
                   if (!success)
                   {
                       _logger.LogError("Unable to import companies house data into database");
                        return await Task.FromResult(false);
                    }
                }

                return await Task.FromResult(true);
            }
            else
            {
                _logger.LogWarning("No entries found in companies house data");
            }

            return await Task.FromResult(false);
        }

        public async Task<bool> ImportCompanyOfficerData()
        {
            var companyNumbers = await _testDataRepository.GetCompaniesListedInUkrlp();

            if (!companyNumbers.Any())
            {
                return await Task.FromResult(false);
            }

            foreach (var companyNumber in companyNumbers)
            {
                var companyDirectors = await _client.GetDirectorCount(companyNumber);
                var companyPSCs = await _client.GetPersonsSignificantControlCount(companyNumber);

                var result = await _testDataRepository.UpdateCompanyOfficerData(companyNumber, companyDirectors, companyPSCs);
                if (!result)
                {
                    return await Task.FromResult(false);
                }
            }

            return await Task.FromResult(true);
        }
    }
}
