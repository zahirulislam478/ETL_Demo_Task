using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETL_API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
