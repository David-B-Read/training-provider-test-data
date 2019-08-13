
namespace TrainingProviderTestData.Application.Configuration
{
    public interface IApplicationConfiguration
    {
        string ConnectionString { get; }
        string CompaniesHouseApiBaseAddress { get; }
        string CompaniesHouseApiKey { get; }
    }
}
