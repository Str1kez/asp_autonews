using asp_autonews.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using asp_autonews.Domain;

namespace asp_autonews.Controllers
{
    public class HomeController : Controller
    {
        private readonly Manager dataManager;

        public HomeController(Manager dataManager)
        {
            this.dataManager = dataManager;
        }

        public IActionResult Index()
        {
            return View(dataManager.InfoFields.GetInfoFieldByKey("Index"));
        }

        public IActionResult Contacts()
        {
            return View(dataManager.InfoFields.GetInfoFieldByKey("Contacts"));
        }
        
    }
}
