
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

    public class CharityDataImporter : ICharityDataImporter
    {
        private readonly ITestDataRepository _testDataRepository;
        private readonly ILogger<CharityDataImporter> _logger;

        public CharityDataImporter(ITestDataRepository testDataRepository, ILogger<CharityDataImporter> logger)
        {
            _testDataRepository = testDataRepository;
            _logger = logger;
        }

        public async Task<bool> ImportCharityData(StreamReader streamReader)
        {
            var entries = new List<CharityDataEntry>();

            using (var csvReader = new CsvReader(streamReader))
            {
                csvReader.Configuration.CultureInfo = CultureInfo.CreateSpecificCulture("en-GB");
                while (csvReader.Read())
                {
                    try
                    {
                        var record = csvReader.GetRecord<CharityDataEntry>();

                        if (record != null)
                        {
                            entries.Add(record);
                        }
                    }
                    catch (TypeConverterException typeConverterException)
                    {
                        _logger.LogError("Unable to load charity commission data due to type conversion error", typeConverterException);
                        return await Task.FromResult(false);
                    }
                    catch (HeaderValidationException headerValidationException)
                    {
                        _logger.LogError("Unable to load charity commission data due to header validation error", headerValidationException);
                        return await Task.FromResult(false);
                    }
                }
            }

            if (entries.Any())
            {
                await _testDataRepository.DeleteCharityData();

                foreach (var entry in entries)
                {
                   bool success = await _testDataRepository.ImportCharityData(entry);
                   if (!success)
                   {
                        _logger.LogError("Unable to import charity commission data into database");
                        return await Task.FromResult(false);
                    }
                }

                return await Task.FromResult(true);
            }
            else
            {
                _logger.LogWarning("No entries found in charity commission data");
            }
            
            return await Task.FromResult(false);
        }
    }
}
