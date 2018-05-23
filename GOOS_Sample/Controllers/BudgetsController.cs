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
        public BudgetsController(IBudgetService service)//BudgetService service)
        {
            this._service = service;
        }
        // GET: Budgets
        public ActionResult Add()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Add(string month, int amount)
        {
            var budgetModel = new BudgetViewModel()
            {
                Month = month,
                Amount = amount
            };
            _service.Add(budgetModel);
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var budgetModels = new List<BudgetViewModel>();
            budgetModels.Add(new BudgetViewModel{ Month = "2015-08", Amount = 500});
            return View(budgetModels);
        }
       
    }
}
