
namespace TrainingProviderTestData.IntegrationTests
{
    using NUnit.Framework;
    using System.IO;
    using Application;
    using TrainingProviderTestData.Application.Repositories;
    using TrainingProviderTestData.Application.Configuration;
    using FluentAssertions;
    using TrainingProviderTestData.Application.Importers;

    [TestFixture]
    public class UkrlpDataImporterTests
    {
        private string _testDataLocation;
        private string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=TrainingProviderData;Integrated Security=True;MultipleActiveResultSets=True;";

        [OneTimeSetUp]
        public void Before_all_tests()
        {
            _testDataLocation = $"{Directory.GetCurrentDirectory()}..\\..\\..\\..\\TestData\\";
        }

        [Test]
        public void Import_succeeds_for_valid_file()
        {
            var fileName = $"{_testDataLocation}UKRLP.xlsx";

            var reader = GetTestDataStreamReader(fileName);

            var importer = new UkrlpDataImporter(new TestDataRepository(new ApplicationConfiguration { ConnectionString = _connectionString }));

            var result = importer.ImportUkrlpData(reader).GetAwaiter().GetResult();

            result.Should().BeTrue();
        }

        [Test]
        public void Import_fails_for_empty_worksheet()
        {
            var fileName = $"{_testDataLocation}UKRLP-empty-worksheet.xlsx";

            var reader = GetTestDataStreamReader(fileName);

            var importer = new UkrlpDataImporter(new TestDataRepository(new ApplicationConfiguration { ConnectionString = _connectionString }));

            var result = importer.ImportUkrlpData(reader).GetAwaiter().GetResult();

            result.Should().BeFalse();
        }

        [Test]
        public void Import_fails_for_file_with_missing_worksheet()
        {
            var fileName = $"{_testDataLocation}UKRLP-missing-worksheet.xlsx";

            var reader = GetTestDataStreamReader(fileName);

            var importer = new UkrlpDataImporter(new TestDataRepository(new ApplicationConfiguration { ConnectionString = _connectionString }));

            var result = importer.ImportUkrlpData(reader).GetAwaiter().GetResult();

            result.Should().BeFalse();
        }

        private StreamReader GetTestDataStreamReader(string fileName)
        {
            var fileStream = File.OpenRead(fileName);

            return new StreamReader(fileStream);
        }
    }
}
