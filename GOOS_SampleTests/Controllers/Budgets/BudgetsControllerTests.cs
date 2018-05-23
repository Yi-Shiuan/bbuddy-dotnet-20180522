using Microsoft.VisualStudio.TestTools.UnitTesting;
using GOOS_Sample.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOOS_Sample.Models;
using GOOS_Sample.Services;
using NSubstitute;

namespace GOOS_Sample.Controllers.Tests
{
    [TestClass()]
    public class BudgetsControllerTests
    {
        [TestMethod()]
        public void AddTest()
        {
            IBudgetService service = Substitute.For<IBudgetService>();
            var budgetsController = new BudgetsController(service);
            budgetsController.Add(2000, "2017-10");
            service.Received().Add(Arg.Is<Budget>(budget => budget.YearMonth == "2017-10" && budget.Amount == 2000));
        }

        [TestMethod()]
        public void IndexTest()
        {
            IBudgetService service = Substitute.For<IBudgetService>();
            var budgetsController = new BudgetsController(service);
            budgetsController.Index();
            service.Received().GetBudgets();
        }
    }
}