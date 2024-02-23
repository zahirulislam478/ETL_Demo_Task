using Microsoft.AspNetCore.Mvc;

namespace ETL_Front.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
