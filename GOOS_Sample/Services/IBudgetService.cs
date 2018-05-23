using System.Collections.Generic;
using GOOS_Sample.Models;

namespace GOOS_Sample.Services
{
    public interface IBudgetService
    {
        void Add(Budget budget);
        IEnumerable<Budget> GetBudgets();
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
    }
}