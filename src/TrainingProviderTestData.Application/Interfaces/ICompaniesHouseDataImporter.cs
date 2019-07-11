
namespace TrainingProviderTestData.Application.Interfaces
{
    using System.IO;
    using System.Threading.Tasks;

    public interface ICompaniesHouseDataImporter
    {
        Task<bool> ImportCompaniesHouseData(StreamReader streamReader);
    }
}
