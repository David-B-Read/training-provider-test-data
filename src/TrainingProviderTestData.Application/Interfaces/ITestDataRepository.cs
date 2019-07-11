
namespace TrainingProviderTestData.Application.Interfaces
{
    using Models;
    using System.Threading.Tasks;

    public interface ITestDataRepository
    {
        Task<bool> ImportCharityData(CharityDataEntry charityData);
        Task DeleteCharityData();
        Task<bool> ImportCompaniesHouseData(CompaniesHouseDataEntry companyData);
        Task DeleteCompaniesHouseData();
    }
}
