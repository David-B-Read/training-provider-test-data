
namespace TrainingProviderTestData.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
     }
}
