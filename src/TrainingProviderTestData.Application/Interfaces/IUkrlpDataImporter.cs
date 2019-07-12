
namespace TrainingProviderTestData.Application.Interfaces
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IUkrlpDataImporter
    {
        Task<bool> ImportUkrlpData(StreamReader streamReader);
    }
}
