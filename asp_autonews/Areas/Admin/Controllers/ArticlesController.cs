using Microsoft.AspNetCore.Mvc;

namespace asp_autonews.Areas.Admin.Controllers
{
    public class ArticlesController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}