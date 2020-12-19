using asp_autonews.Domain;
using Microsoft.AspNetCore.Mvc;

namespace asp_autonews.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly Manager dataManager;

        public HomeController(Manager dataManager)
        {
            this.dataManager = dataManager;
        }
        public IActionResult Index()
        {
            return View(dataManager.Articles.GetArticles());
        }
    }
}