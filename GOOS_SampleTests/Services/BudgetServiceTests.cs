using Microsoft.VisualStudio.TestTools.UnitTesting;
using GOOS_Sample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using GOOS_Sample.Models;
using NSubstitute;

namespace GOOS_Sample.Services.Tests
{
    [TestClass()]
    public class BudgetServiceTests
    {
        [TestMethod()]
        public void AddTest()
        {
            IBudgetRepo repo = Substitute.For<IBudgetRepo>();
            var service = new BudgetService(repo);

            var budget = new Budget
            {
                Amount = 2000,
                YearMonth = "2017-10"
            };
            service.Add(budget);

            repo.Received().Save(budget);
        }

        [TestMethod()]
        public void GetBudgetsTest()
        {
            var repo = Substitute.For<IBudgetRepo>();

            var service = new BudgetService(repo);
            
            service.GetBudgets();

            repo.Received().GetBudgets();
        }
    }
}