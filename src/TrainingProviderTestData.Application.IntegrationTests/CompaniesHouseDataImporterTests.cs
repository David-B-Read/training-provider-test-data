
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
    using TrainingProviderTestData.Application;
    using System.Net.Http;

    [TestFixture]
    public class CompaniesHouseDataImporterTests
    {
        private string _testDataLocation;
        private string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=TrainingProviderData;Integrated Security=True;MultipleActiveResultSets=True;";
        private Mock<ILogger<CompaniesHouseDataImporter>> _importLogger;
        private Mock<ILogger<TestDataRepository>> _repoLogger;
        private CompaniesHouseApiClient _client;
        private Mock<ILogger<CompaniesHouseApiClient>> _apiClientLogger;
        private ApplicationConfiguration _config;

        [SetUp]
        public void Before_all_tests()
        {
            _testDataLocation = $"{Directory.GetCurrentDirectory()}..\\..\\..\\..\\TestData\\";
            _importLogger = new Mock<ILogger<CompaniesHouseDataImporter>>();
            _repoLogger = new Mock<ILogger<TestDataRepository>>();
            var apiConfig = new ApplicationConfiguration();
            apiConfig.CompaniesHouseApiBaseAddress = "https://api.companieshouse.gov.uk";
            // ADD YOUR API KEY HERE - go to https://developer.companieshouse.gov.uk/api/docs/ to set up an account
            apiConfig.CompaniesHouseApiKey = "";
            _apiClientLogger = new Mock<ILogger<CompaniesHouseApiClient>>();
            _client = new CompaniesHouseApiClient(_apiClientLogger.Object, new HttpClient(), apiConfig);
            _config = new ApplicationConfiguration{ ConnectionString = _connectionString };
        }
        
        [Test]
        public void Import_fails_for_empty_file()
        {
            var fileName = $"{_testDataLocation}CompaniesHouse-empty.csv";

            var reader = GetTestDataStreamReader(fileName);

            var importer = new CompaniesHouseDataImporter(new TestDataRepository(_config, _repoLogger.Object), _importLogger.Object, _client);

            var result = importer.ImportCompaniesHouseData(reader).GetAwaiter().GetResult();

            result.Should().BeFalse();
        }

        [Test]
        public void Import_fails_for_file_with_no_records()
        {
            var fileName = $"{_testDataLocation}CompaniesHouse-no-records.csv";

            var reader = GetTestDataStreamReader(fileName);

            var importer = new CompaniesHouseDataImporter(new TestDataRepository(_config, _repoLogger.Object), _importLogger.Object, _client);

            var result = importer.ImportCompaniesHouseData(reader).GetAwaiter().GetResult();

            result.Should().BeFalse();
        }

        [Test]
        public void Import_succeeds_for_valid_file()
        {
            var fileName = $"{_testDataLocation}CompaniesHouse.csv";

            var reader = GetTestDataStreamReader(fileName);

            var importer = new CompaniesHouseDataImporter(new TestDataRepository(_config, _repoLogger.Object), _importLogger.Object, _client);

            var result = importer.ImportCompaniesHouseData(reader).GetAwaiter().GetResult();

            result.Should().BeTrue();
        }

        [Test]
        public void Import_updates_officer_info_for_companies_in_ukrlp()
        {
            var importer = new CompaniesHouseDataImporter(new TestDataRepository(_config, _repoLogger.Object), _importLogger.Object, _client);

            var result = importer.ImportCompanyOfficerData().GetAwaiter().GetResult();

            result.Should().BeTrue();
        }

        private StreamReader GetTestDataStreamReader(string fileName)
        {
            var fileStream = File.OpenRead(fileName);

            return new StreamReader(fileStream);
        }
    }
}
