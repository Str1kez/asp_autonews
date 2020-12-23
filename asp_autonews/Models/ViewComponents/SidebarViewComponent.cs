using System.Threading.Tasks;
using asp_autonews.Domain;
using Microsoft.AspNetCore.Mvc;

namespace asp_autonews.Models.ViewComponents
{
    // код для сайдбара на всех страницах
    public class SidebarViewComponent : ViewComponent
    {
        private readonly Manager dataManager;

        public SidebarViewComponent(Manager dataManager)
        {
            this.dataManager = dataManager;
        }

        public Task<IViewComponentResult> InvokeAsync()
        {
            return Task.FromResult((IViewComponentResult)View("Default", dataManager.Articles.GetArticles()));
        }
    }
}