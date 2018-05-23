using Microsoft.VisualStudio.TestTools.UnitTesting;
using GOOS_Sample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOOS_Sample.Services.Tests
{
    [TestClass()]
    public class BudgetQueryService_Tests
    {
        [TestMethod()]
        public void GetTotalBudget_Test()
        {
            BudgetQueryService service = new BudgetQueryService();
            var actual = service.GetTotalBudget("2018-05-16", "2018-07-15");
            var expected = 1060;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetTotalBudget_Test_SameDate()
        {
            BudgetQueryService service = new BudgetQueryService();
            var actual = service.GetTotalBudget("2018-05-16", "2018-05-16");
            var expected = 10;
            Assert.AreEqual(expected, actual);
        }
    }
}