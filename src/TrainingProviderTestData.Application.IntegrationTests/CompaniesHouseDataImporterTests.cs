
namespace TrainingProviderTestData.IntegrationTests
{
    using System.IO;
    using NUnit.Framework;
    using Application.Configuration;
    using Application.Repositories;
    using FluentAssertions;
    using Microsoft.Extensions.Logging;
    using Moq;
    using TrainingProviderTestData.Application.Importers;

    [TestFixture]
    public class CompaniesHouseDataImporterTests
    {
        private string _testDataLocation;
        private string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=TrainingProviderData;Integrated Security=True;MultipleActiveResultSets=True;";
        private Mock<ILogger<CompaniesHouseDataImporter>> _importLogger;
        private Mock<ILogger<TestDataRepository>> _repoLogger;
        private ApplicationConfiguration _config;

        [OneTimeSetUp]
        public void Before_all_tests()
        {
            _testDataLocation = $"{Directory.GetCurrentDirectory()}..\\..\\..\\..\\TestData\\";
            _importLogger = new Mock<ILogger<CompaniesHouseDataImporter>>();
            _repoLogger = new Mock<ILogger<TestDataRepository>>();
            _config = new ApplicationConfiguration{ ConnectionString = _connectionString };
        }
        
        [Test]
        public void Import_fails_for_empty_file()
        {
            var fileName = $"{_testDataLocation}CompaniesHouse-empty.csv";

            var reader = GetTestDataStreamReader(fileName);

            var importer = new CompaniesHouseDataImporter(new TestDataRepository(_config, _repoLogger.Object), _importLogger.Object);

            var result = importer.ImportCompaniesHouseData(reader).GetAwaiter().GetResult();

            result.Should().BeFalse();
        }

        [Test]
        public void Import_fails_for_file_with_no_records()
        {
            var fileName = $"{_testDataLocation}CompaniesHouse-no-records.csv";

            var reader = GetTestDataStreamReader(fileName);

            var importer = new CompaniesHouseDataImporter(new TestDataRepository(_config, _repoLogger.Object), _importLogger.Object);

            var result = importer.ImportCompaniesHouseData(reader).GetAwaiter().GetResult();

            result.Should().BeFalse();
        }

        [Test]
        public void Import_succeeds_for_valid_file()
        {
            var fileName = $"{_testDataLocation}CompaniesHouse.csv";

            var reader = GetTestDataStreamReader(fileName);

            var importer = new CompaniesHouseDataImporter(new TestDataRepository(_config, _repoLogger.Object), _importLogger.Object);

            var result = importer.ImportCompaniesHouseData(reader).GetAwaiter().GetResult();

            result.Should().BeTrue();
        }

        private StreamReader GetTestDataStreamReader(string fileName)
        {
            var fileStream = File.OpenRead(fileName);

            return new StreamReader(fileStream);
        }
    }
}
