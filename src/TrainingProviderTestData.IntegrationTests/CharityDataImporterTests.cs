
namespace TrainingProviderTestData.IntegrationTests
{
    using System.IO;
    using NUnit.Framework;
    using System.Reflection;
    using Application.Configuration;
    using Application.Repositories;
    using TrainingProviderTestData.Application;
    using FluentAssertions;

    [TestFixture]
    public class CharityDataImporterTests
    {
        private string _testDataLocation;
        private string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=TrainingProviderData;Integrated Security=True;MultipleActiveResultSets=True;";

        [OneTimeSetUp]
        public void Before_all_tests()
        {
            _testDataLocation = $"{Directory.GetCurrentDirectory()}..\\..\\..\\..\\TestData\\";
        }
        
        [Test]
        public void Import_fails_for_empty_file()
        {
            var fileName = $"{_testDataLocation}charities-empty.csv";

            var reader = GetTestDataStreamReader(fileName);

            var importer = new CharityDataImporter(new TestDataRepository(new ApplicationConfiguration { ConnectionString = _connectionString}));

            var result = importer.ImportCharityData(reader).GetAwaiter().GetResult();

            result.Should().BeFalse();
        }

        [Test]
        public void Import_fails_for_file_with_no_records()
        {
            var fileName = $"{_testDataLocation}charities-no-records.csv";

            var reader = GetTestDataStreamReader(fileName);

            var importer = new CharityDataImporter(new TestDataRepository(new ApplicationConfiguration { ConnectionString = _connectionString }));

            var result = importer.ImportCharityData(reader).GetAwaiter().GetResult();

            result.Should().BeFalse();
        }

        [Test]
        public void Import_succeeds_for_valid_file()
        {
            var fileName = $"{_testDataLocation}charities.csv";

            var reader = GetTestDataStreamReader(fileName);

            var importer = new CharityDataImporter(new TestDataRepository(new ApplicationConfiguration { ConnectionString = _connectionString }));

            var result = importer.ImportCharityData(reader).GetAwaiter().GetResult();

            result.Should().BeTrue();
        }

        private StreamReader GetTestDataStreamReader(string fileName)
        {
            var fileStream = File.OpenRead(fileName);

            return new StreamReader(fileStream);
        }
    }
}
