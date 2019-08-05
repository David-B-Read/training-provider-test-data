
using Microsoft.Extensions.Configuration;

namespace TrainingProviderTestData.Application.Configuration
{
    public static class ConfigurationFactory
    {
        public static IApplicationConfiguration GetApplicationConfiguration(IConfiguration configuration)
        {
            return new ApplicationConfiguration
            {
                ConnectionString = configuration["ConnectionString"],
                ApiBaseAddress = configuration["ApiBaseAddress"]
            };
        }
    }
}
