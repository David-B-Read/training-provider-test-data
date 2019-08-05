
namespace TrainingProviderTestData.Web.Controllers
{
    using System.IO;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("upload")]
    public class UploadController : Controller
    {
        private readonly IUkrlpDataImporter _ukrlpDataImporter;
        private readonly ICompaniesHouseDataImporter _companiesHouseDataImporter;
        private readonly ICharityDataImporter _charityDataImporter;

        public UploadController(IUkrlpDataImporter ukrlpDataImporter, 
                                ICompaniesHouseDataImporter companiesHouseDataImporter, 
                                ICharityDataImporter charityDataImporter)
        {
            _ukrlpDataImporter = ukrlpDataImporter;
            _companiesHouseDataImporter = companiesHouseDataImporter;
            _charityDataImporter = charityDataImporter;
        }

        [HttpPost("ukrlp")]
        public async Task<IActionResult> UploadUkrlp()
        {
            var ukrlpFile = GetFile();

            MemoryStream stream = new MemoryStream();
            ukrlpFile.CopyTo(stream);
            stream.Position = 0;

            var result = await _ukrlpDataImporter.ImportUkrlpData(new StreamReader(stream));

            return Ok(result);
        }

        [HttpPost("companies-house")]
        public async Task<IActionResult> UploadCompaniesHouse()
        {
            var companiesHouseFile = GetFile();

            MemoryStream stream = new MemoryStream();
            companiesHouseFile.CopyTo(stream);
            stream.Position = 0;

            var result = await _companiesHouseDataImporter.ImportCompaniesHouseData(new StreamReader(stream));

            return Ok(result);
        }

        [HttpPost("charity-commission")]
        public async Task<IActionResult> UploadCharityCommission()
        {
            var charityCommissionFile = GetFile();

            MemoryStream stream = new MemoryStream();
            charityCommissionFile.CopyTo(stream);
            stream.Position = 0;

            var result = await _charityDataImporter.ImportCharityData(new StreamReader(stream));

            return Ok(result);
        }

        private IFormFile GetFile()
        {
            if (Request.Form.Files == null || Request.Form.Files.Count == 0)
            {
                return null;
            }

            var file = Request.Form.Files[0];

            if (file == null || file.Length == 0)
            {
                return null;
            }

            return file;
        }
    }
}
