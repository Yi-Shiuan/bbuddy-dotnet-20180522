using System;
using System.Collections.Generic;
using System.Linq;
using GOOS_Sample.Controllers;
using GOOS_Sample.Models;
using GOOS_Sample.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace GOOS_SampleTests.Controllers.GetBudgets
{
    [TestClass]
    public class BudgetsControllerTest
    {
        private readonly List<Budget> budgets = new List<Budget> 
        {
            new Budget{Amount = 560, YearMonth = "2016-02"},
            new Budget{Amount = 600, YearMonth = "2018-04"},
            new Budget{Amount = 620, YearMonth = "2018-05"},
            new Budget{Amount = 620, YearMonth = "2018-07"}
        };

        private readonly IBudgetRepo repo = Substitute.For<IBudgetRepo>();

        private IBudgetService service;

        [TestInitialize]
        public void Start()
        {
            repo.GetBudgets(Arg.Any<string>())
                .Returns(info => budgets.FirstOrDefault(x => x.YearMonth == info.Arg<string>()));
            service = new BudgetService(repo);
        }

        [TestMethod]
        public void Get_Budget_By_DateRange_0415_To_0515()
        {
            var start = new DateTime(2018,04, 15);
            var end = new DateTime(2018, 05, 15);
            var budget = service.GetBudgets(start, end);

            Assert.AreEqual(620, budget);
        }

        [TestMethod]
        public void Get_Budget_By_DateRange_0415_To_0630()
        {
            var start = new DateTime(2018, 04, 15);
            var end = new DateTime(2018, 06, 30);
            var budget = service.GetBudgets(start, end);

            Assert.AreEqual(940, budget);
        }

        [TestMethod]
        public void Get_Budget_By_DateRange_0520_To_0716()
        {
            var start = new DateTime(2018, 05, 20);
            var end = new DateTime(2018, 07, 16);
            var budget = service.GetBudgets(start, end);

            Assert.AreEqual(560, budget);
        }

        [TestMethod]
        public void Get_Budget_By_DateRange_0115_To_0213()
        {
            var start = new DateTime(2016, 01, 15);
            var end = new DateTime(2016, 02, 13);
            var budget = service.GetBudgets(start, end);

            Assert.AreEqual(247, budget);
        }

        [TestMethod]
        public void Get_Budget_By_DateRange_0201_To_0213()
        {
            var start = new DateTime(2016, 02, 01);
            var end = new DateTime(2016, 02, 13);
            var budget = service.GetBudgets(start, end);

            Assert.AreEqual(247, budget);
        }

        [TestMethod]
        public void Get_Budget_By_DateRange_0301_To_0213()
        {
            var start = new DateTime(2016, 02, 15);
            var end = new DateTime(2016, 02, 13);
            var budget = service.GetBudgets(start, end);

            Assert.AreEqual(0, budget);
        }
    }
}
