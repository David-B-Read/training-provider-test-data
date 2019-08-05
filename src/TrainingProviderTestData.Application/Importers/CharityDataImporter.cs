
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

    public class CharityDataImporter : ICharityDataImporter
    {
        private readonly ITestDataRepository _testDataRepository;

        public CharityDataImporter(ITestDataRepository testDataRepository)
        {
            _testDataRepository = testDataRepository;
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
                await _testDataRepository.DeleteCharityData();

                foreach (var entry in entries)
                {
                   bool success = await _testDataRepository.ImportCharityData(entry);
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
