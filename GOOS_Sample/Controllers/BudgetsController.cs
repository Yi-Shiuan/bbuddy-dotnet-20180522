using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GOOS_Sample.Models;
using GOOS_Sample.Services;

namespace GOOS_Sample.Controllers
{
    public class BudgetsController : Controller
    {
        private readonly IBudgetService _service;

        public BudgetsController(IBudgetService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(_service.GetBudgets());
        }
        
        [HttpGet]
        public ActionResult Add()
        {
            return View();

        }
        
        [HttpPost]
        public ActionResult Add(int amount, string yearMonth)
        {
            _service.Add(new Budget{Amount = amount, YearMonth = yearMonth});
            return RedirectToAction("Index");
        }
    }
}