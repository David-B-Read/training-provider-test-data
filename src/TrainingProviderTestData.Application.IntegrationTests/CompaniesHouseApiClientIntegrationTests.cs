
using NUnit.Framework;

namespace TrainingProviderTestData.Application.IntegrationTests
{
    using System.Net.Http;
    using Configuration;
    using FluentAssertions;
    using Microsoft.Extensions.Logging;
    using Moq;

    [TestFixture]
    public class CompaniesHouseApiClientIntegrationTests
    {
        private CompaniesHouseApiClient _client;
        private Mock<ILogger<CompaniesHouseApiClient>> _logger;

        [SetUp]
        public void Before_each_test()
        { 
            var config = new ApplicationConfiguration();
            config.CompaniesHouseApiBaseAddress = "https://api.companieshouse.gov.uk";
            // ADD YOUR API KEY HERE - go to https://developer.companieshouse.gov.uk/api/docs/ to set up an account
            config.CompaniesHouseApiKey = ""; 
            _logger = new Mock<ILogger<CompaniesHouseApiClient>>();
            _client = new CompaniesHouseApiClient(_logger.Object, new HttpClient(), config);
        }

        [Test]
        public void Companies_house_API_retrieves_company_summary()
        {
            var companyNumber = "00233462";

            var companySummary = _client.GetCompanySummary(companyNumber).GetAwaiter().GetResult();

            companySummary.company_status.Should().Be("active");
        }

        [Test]
        public void Companies_house_API_retrieves_number_of_active_directors()
        {
            var companyNumber = "00233462";

            var directorsCount = _client.GetDirectorCount(companyNumber).GetAwaiter().GetResult();

            directorsCount.Should().BeGreaterThan(0);
        }

        [Test]
        public void Companies_house_API_retrieves_number_of_active_PSCs()
        {
            var companyNumber = "00233462";

            var pscCount = _client.GetPersonsSignificantControlCount(companyNumber).GetAwaiter().GetResult();

            pscCount.Should().BeGreaterThan(0);
        }
    }
}
