
namespace TrainingProviderTestData.Application
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Clients;
    using Configuration;
    using Interfaces;
    using System.Net;
    using Newtonsoft.Json;

    public class CompaniesHouseApiClient : ICompaniesHouseApiClient
    {
        private readonly ILogger<CompaniesHouseApiClient> _logger;
        private readonly HttpClient _httpClient;
        private readonly IApplicationConfiguration _config;

        public CompaniesHouseApiClient(ILogger<CompaniesHouseApiClient> logger, HttpClient httpClient, IApplicationConfiguration config)
        {
            _logger = logger;
            _httpClient = httpClient;
            _config = config;
            _httpClient.BaseAddress = new Uri(_config.CompaniesHouseApiBaseAddress);
            _httpClient.DefaultRequestHeaders.Authorization = BuildAuthorizationHeader();
        }

        public async Task<CompanyDetails> GetCompanySummary(string companyNumber)
        {
            _logger.LogInformation($"Retrieving company summary for company {companyNumber}");

            using (var responseMessage = await _httpClient.GetAsync($"/company/{companyNumber}"))
            {
                if (responseMessage.StatusCode != HttpStatusCode.OK && responseMessage.StatusCode != HttpStatusCode.NotFound)
                {   
                    // TODO: use Polly
                    throw new HttpRequestException(
                        $"Unable to retrieve company data from Companies House API - Status Code {responseMessage.StatusCode}");
                }

                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                if (jsonData != null)
                {
                    return await Task.FromResult(JsonConvert.DeserializeObject<CompanyDetails>(jsonData));
                }

                return await Task.FromResult<CompanyDetails>(null);
            }
        }

        public async Task<int> GetDirectorCount(string companyNumber)
        {
            _logger.LogInformation($"Retrieving company officers for company {companyNumber}");

            using (var responseMessage = await _httpClient.GetAsync($"/company/{companyNumber}/officers?items_per_page=999"))
            {
                if (responseMessage.StatusCode != HttpStatusCode.OK && responseMessage.StatusCode != HttpStatusCode.NotFound)
                {
                    // TODO: use Polly
                    throw new HttpRequestException(
                        $"Unable to retrieve officer data from Companies House API - Status Code {responseMessage.StatusCode}");
                }

                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                if (jsonData != null)
                {
                    var officerData = JsonConvert.DeserializeObject<OfficerList>(jsonData);

                    var activeDirectors = officerData.items.Where(x => x.officer_role?.ToLower() == "director" && !x.resigned_on.HasValue).Count();

                    return await Task.FromResult<int>(activeDirectors);
                }

                return await Task.FromResult<int>(0);
            }
        }

        public async Task<int> GetPersonsSignificantControlCount(string companyNumber)
        {
            _logger.LogInformation($"Retrieving PSCs for company {companyNumber}");

            using (var responseMessage = await _httpClient.GetAsync($"/company/{companyNumber}/persons-with-significant-control?items_per_page=999"))
            {
                if (responseMessage.StatusCode != HttpStatusCode.OK && responseMessage.StatusCode != HttpStatusCode.NotFound)
                {
                    // TODO: use Polly
                    throw new HttpRequestException(
                        $"Unable to retrieve PSC data from Companies House API - Status Code {responseMessage.StatusCode}");
                }

                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                if (jsonData != null)
                {
                    var officerData = JsonConvert.DeserializeObject<PersonWithSignificantControlList>(jsonData);
                    
                    return await Task.FromResult<int>(officerData.active_count);
                }

                return await Task.FromResult<int>(0);
            }
        }

        private AuthenticationHeaderValue BuildAuthorizationHeader()
        {
            var headerBytes = Encoding.ASCII.GetBytes(_config.CompaniesHouseApiKey);
            var authenticationToken = Convert.ToBase64String(headerBytes);
            return new AuthenticationHeaderValue("Basic", authenticationToken);
        }
    }
}
