
namespace TrainingProviderTestData.Application.Importers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Text;
    using ExcelDataReader;
    using Interfaces;
    using Models;

    public class UkrlpDataImporter : IUkrlpDataImporter
    {
        private readonly ITestDataRepository _testDataRepository;

        public UkrlpDataImporter(ITestDataRepository testDataRepository)
        {
            _testDataRepository = testDataRepository;
        }

        public async Task<bool> ImportUkrlpData(StreamReader streamReader)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            
            List<UkrlpDataEntry> entries = new List<UkrlpDataEntry>();

            using (var reader = ExcelReaderFactory.CreateReader(streamReader.BaseStream))
            {
                // skip the first 6 sheets until we get to the 'UKRLP Data' sheet
                for (var sheet = 1; sheet <= 6; sheet++)
                {
                    reader.NextResult();
                }

                // skip the first 4 rows that contain summary information
                for (var row = 1; row <= 4; row++)
                {
                    reader.Read();
                }

                try
                {
                    while (reader.Read())
                    {
                        var entry = new UkrlpDataEntry();
                        entry.UKPRN = reader.GetDouble(0).ToString();
                        entry.LegalName = reader.GetString(1);
                        entry.TradingName = reader.GetString(2);
                        entry.Status = reader.GetString(4);
                        entry.PrimaryVerificationSource = reader.GetString(7);
                        entry.CompanyNumber = reader.GetString(8);
                        entry.CharityNumber = reader.GetString(9);

                        entries.Add(entry);
                    }
                }
                catch (NullReferenceException)
                {
                    // log

                    return await Task.FromResult(false);
                }

                if (entries.Any())
                {
                    await _testDataRepository.DeleteUkrlpData();

                    foreach (var entry in entries)
                    {
                        bool success = await _testDataRepository.ImportUkrlpData(entry);
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
}
