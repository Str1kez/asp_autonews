using System;
using asp_autonews.Domain;
using Microsoft.AspNetCore.Mvc;

namespace asp_autonews.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly Manager dataManager;

        public ArticlesController(Manager dataManager)
        {
            this.dataManager = dataManager;
        }

        // GET
        public IActionResult Index(Guid id)
        {
            // если хотим посмотреть только один блок новостей (это как рекурсия)
            if (id != default)
                return View("Show", dataManager.Articles.GetArticleById(id));
            // рассматриваем все новости
            ViewBag.InfoField = dataManager.InfoFields.GetInfoFieldByKey("Articles");
            return View(dataManager.Articles.GetArticles());
        }   
    }
}