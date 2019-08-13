
namespace TrainingProviderTestData.Application.Interfaces
{
    using System.Collections;
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
        Task<bool> UpdateCompanyOfficerData(string companyNumber, int directorsCount, int pscCount);
    }
}
