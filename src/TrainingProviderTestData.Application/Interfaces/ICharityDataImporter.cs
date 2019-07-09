
namespace TrainingProviderTestData.Application.Interfaces
{
    using System.IO;
    using System.Threading.Tasks;

    public interface ICharityDataImporter
    {
        Task<bool> ImportCharityData(StreamReader streamReader);
    }
}
