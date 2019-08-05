
namespace TrainingProviderTestData.Application.Configuration
{
    public interface IApplicationConfiguration
    {
        string ConnectionString { get; }
        string ApiBaseAddress { get; set; }
    }
}
