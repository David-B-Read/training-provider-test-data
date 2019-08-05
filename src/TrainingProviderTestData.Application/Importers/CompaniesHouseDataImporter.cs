
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
    using TrainingProviderTestData.Application.Models;

    public class CompaniesHouseDataImporter : ICompaniesHouseDataImporter
    {
        private readonly ITestDataRepository _testDataRepository;

        public CompaniesHouseDataImporter(ITestDataRepository testDataRepository)
        {
            _testDataRepository = testDataRepository;
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
                        // log
                        return await Task.FromResult(false);
                    }
                    catch (HeaderValidationException headerValidationException)
                    {
                        // log
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
                        // log
                        return await Task.FromResult(false);
                    }
                }

                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }
    }
}
