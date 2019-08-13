
namespace TrainingProviderTestData.Application.Interfaces
{
    using System.Threading.Tasks;
    using Clients;

    public interface ICompaniesHouseApiClient
    {
        Task<CompanyDetails> GetCompanySummary(string companyNumber);
        Task<int> GetDirectorCount(string companyNumber);
        Task<int> GetPersonsSignificantControlCount(string companyNumber);
    }
}
