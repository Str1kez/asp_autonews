using System;
using System.IO;
using asp_autonews.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asp_autonews.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticlesController : Controller
    {
        private readonly Manager dataManager;
        private readonly IWebHostEnvironment hostEnvironment;

        public ArticlesController(Manager dataManager, IWebHostEnvironment hostEnvironment)
        {
            this.dataManager = dataManager;
            this.hostEnvironment = hostEnvironment;
        }

        public IActionResult Edit(Guid id)
        {   
            // Если айди дефолтный = новый -> создаем статью, иначе берем её из БД
            var entity = id == default ? new Domain.Entities.Article() : dataManager.Articles.GetArticleById(id);
            return View(entity);
        }

        [HttpPost]
        // IFormFile Для подгрузки картинок
        public  IActionResult Edit (Domain.Entities.Article model, IFormFile img)
        {
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    model.Image = img.FileName;
                    using (var s = new FileStream(Path.Combine(hostEnvironment.WebRootPath, "img/", img.FileName),
                        FileMode.Create) )
                    {
                        img.CopyTo(s);
                    }
                }
                // Сохраняем и переходим на главную страницу в админке
                dataManager.Articles.SaveArticle(model);
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController)
                    .Replace("Controller", ""));
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            dataManager.Articles.DeleteArticle(id);
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController)
                .Replace("Controller", ""));
        }
    }
}