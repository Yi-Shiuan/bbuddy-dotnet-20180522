using Microsoft.VisualStudio.TestTools.UnitTesting;
using GOOS_Sample.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using GOOS_Sample.Services;
using NSubstitute;

namespace GOOS_Sample.Controllers.Tests
{
    [TestClass()]
    public class BudgetsController_Tests
    {
        [TestMethod()]
        public void Add_Test()
        {
            var mockService = Substitute.For<BudgetService>();
            var controller = new BudgetsController(mockService);
            controller.Add();

            Assert.IsTrue(mockService.ReceivedCalls().Any());
        }
    }
}