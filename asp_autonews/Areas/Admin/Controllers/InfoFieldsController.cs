using asp_autonews.Domain;
using asp_autonews.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace asp_autonews.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InfoFieldsController : Controller
    {
        private readonly Manager dataManager;

        public InfoFieldsController(Manager dataManager)
        {
            this.dataManager = dataManager;
        }

        public IActionResult Edit(string key)
        {
            // ищем в БД это текстовое поле и возвращаем в представление
            var entity = dataManager.InfoFields.GetInfoFieldByKey(key);
            return View(entity);
        }

        [HttpPost]
        public IActionResult Edit(InfoField model)
        {
            if (ModelState.IsValid)
            {
                // Если модель верная, то сохраняем в БД и перенаправляем на начальную страницу панели
                dataManager.InfoFields.SaveInfoField(model);
                return RedirectToAction(nameof(HomeController.Index),
                    nameof(HomeController).Replace("Controller", ""));
            }

            return View(model);
        }
    }
}