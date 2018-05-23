using Microsoft.VisualStudio.TestTools.UnitTesting;
using GOOS_Sample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOOS_Sample.Models;
using NSubstitute;
using System.Data.Entity;
using FluentAssertions;

namespace GOOS_Sample.Services.Tests
{
    [TestClass()]
    public class BudgetRepoTests
    {
        [TestMethod()]
        public void SaveTest()
        {
            var customers =
                new List<Budget>
                {
                    new Budget(),
                    new Budget(),
                    new Budget(),
                    new Budget(),
                    new Budget()
                }.AsQueryable();
            var customerDbSet = Substitute.For<IDbSet<Budget>>();
            customerDbSet.Provider.Returns(customers.Provider);
            customerDbSet.Expression.Returns(customers.Expression);
            customerDbSet.ElementType.Returns(customers.ElementType);
            customerDbSet.GetEnumerator().Returns(customers.GetEnumerator());

            var context = Substitute.For<ApplicationDbContext>();
            context.Budgets.Returns(customerDbSet);
            var repo = new BudgetRepo(context);

            repo.Save(new Budget());

            context.Received().SaveChanges();
        }

        [TestMethod()]
        public void GetBudgetsTest()
        {
            /* Arrange */
            var budget = new Budget
            {
                Amount = 2000,
                YearMonth = "2017-10"
            };
            var customers =
                new List<Budget>
                {
                    new Budget(),
                    new Budget(),
                    new Budget(),
                    new Budget(),
                    new Budget(),
                    budget
                }.AsQueryable();

            var customerDbSet = Substitute.For<IDbSet<Budget>>();
            customerDbSet.Provider.Returns(customers.Provider);
            customerDbSet.Expression.Returns(customers.Expression);
            customerDbSet.ElementType.Returns(customers.ElementType);
            customerDbSet.GetEnumerator().Returns(customers.GetEnumerator());

            var context = Substitute.For<ApplicationDbContext>();
            context.Budgets.Returns(customerDbSet);

            
            var repo = new BudgetRepo(context);
            
            var result = repo.GetBudgets();

            result.Should().Contain(budget);
        }
    }
}