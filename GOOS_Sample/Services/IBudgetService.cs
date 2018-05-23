using System;
using System.Collections.Generic;
using System.Linq;
using GOOS_Sample.Models;

namespace GOOS_Sample.Services
{
    public interface IBudgetService
    {
        void Add(Budget budget);
        IEnumerable<Budget> GetBudgets();
        int GetBudgets(DateTime start, DateTime end);
    }

    public class BudgetService : IBudgetService
    {
        private readonly IBudgetRepo repo;
        public BudgetService(IBudgetRepo repo)
        {
            this.repo = repo;
        }

        public void Add(Budget budget)
        {
            repo.Save(budget);
        }

        public IEnumerable<Budget> GetBudgets()
        {
            return repo.GetBudgets();
        }

        public int GetBudgets(DateTime start, DateTime end)
        {
            var budget = 0;
            int avg;
            Budget monthBudget;
            while (!(start > end))
            {
                monthBudget = repo.GetBudgets(start.ToString("yyyy-MM"));
                if (monthBudget != null)
                {
                    avg = monthBudget.Amount / Days(start);
                    budget += avg;
                }

                start = start.AddDays(1);
            }

            return budget;
        }

        private int Days(DateTime start)
        {
            return new DateTime(start.Year, start.Month + 1, 1).Subtract(new DateTime(start.Year, start.Month, 1)).Days;
        }
    }
}