using System.Collections.Generic;
using GOOS_Sample.Models;

namespace GOOS_Sample.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly IBudgetRepo _repo;

        public BudgetService(IBudgetRepo repo)
        {
            this._repo = repo;
        }
//        List<BudgetViewModel> budgets = new List<BudgetViewModel>();
        public void Add(BudgetViewModel budgetViewInput)
        {
            //budgets.Add(budget);
            _repo.Add(new Budget
            {
                Amount = budgetViewInput.Amount,
                YearMonth = budgetViewInput.Month
            });
        }
    }

    public interface IBudgetService
    {
        void Add(BudgetViewModel budget);
    }
}