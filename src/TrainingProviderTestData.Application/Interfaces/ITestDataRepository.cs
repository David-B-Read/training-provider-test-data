
namespace TrainingProviderTestData.Application.Interfaces
{
    using Models;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface ITestDataRepository
    {
        Task<bool> ImportCharityData(CharityDataEntry charityData);
        Task DeleteCharityData();
        Task<bool> ImportCompaniesHouseData(CompaniesHouseDataEntry companyData);
        Task DeleteCompaniesHouseData();
        Task<bool> ImportUkrlpData(UkrlpDataEntry ukrlpData);
        Task DeleteUkrlpData();
        Task<IEnumerable<string>> GetCompaniesListedInUkrlp();
        Task UpdateCompanyOfficerData(string companyNumber, int directorsCount, int pscCount);
    }
}
