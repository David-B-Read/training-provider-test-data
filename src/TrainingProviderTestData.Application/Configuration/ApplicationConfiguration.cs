
namespace TrainingProviderTestData.Application.Configuration
{
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        public string ConnectionString { get; set; }
        public string CompaniesHouseApiBaseAddress { get; set; }
        public string CompaniesHouseApiKey { get; set; }
    }
}
