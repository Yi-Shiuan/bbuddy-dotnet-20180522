using System;
using System.Collections.Generic;
using System.Linq;
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

        public int CalculateTotalInRange(string startDate, string endDate, List<BudgetViewModel> budgets)
        {
            return CalculateTotalInRange(new DateRangeService {SatrtDate = startDate, EndDate = endDate}, budgets);
        }

        public int CalculateTotalInRange(DateRangeService range, List<BudgetViewModel> budgets)
        {
            var total = 0;
            var selectedBudgets = SelectBudgets(range, budgets);
            if (range.IsRangeInSameYearMonth() && selectedBudgets.Count > 0)
            {
                var days = (DateTime.Parse(range.EndDate) - DateTime.Parse(range.SatrtDate)).Days + 1;
                total = days * CalculateAverageBudget(selectedBudgets[0]);
                return total;
            }
            for (var idx = 0; idx < selectedBudgets.Count; idx++)
            {
                var budget = selectedBudgets[idx];
                var budgetYear = Convert.ToInt32(budget.Month.Split('-')[0]);
                var budgetMonth = Convert.ToInt32(budget.Month.Split('-')[1]);
                if (range.IsYearMonthFullInRange(budgetYear, budgetMonth))
                {
                    total += budget.Amount;
                }
                else if (range.IsYearMonthOnFirstMonthOfRange(budgetYear, budgetMonth))
                {
                    var days = ((new DateTime(budgetYear, budgetMonth, DateTime.DaysInMonth(budgetYear, budgetMonth))) -
                                DateTime.Parse(range.SatrtDate)).Days + 1;
                    total += days * CalculateAverageBudget(budget);
                }
                else if (range.IsYearMonthOnLastMonthOfRange(budgetYear, budgetMonth))
                {
                    var days = (DateTime.Parse(range.EndDate) -
                                (new DateTime(budgetYear, budgetMonth, 1))).Days + 1;
                    total += days * CalculateAverageBudget(budget);
                }
            }

            return total;
        }

        public int CalculateAverageBudget(BudgetViewModel budget)
        {
            var budgetYear = Convert.ToInt32(budget.Month.Split('-')[0]);
            var budgetMonth = Convert.ToInt32(budget.Month.Split('-')[1]);
            return Convert.ToInt32(budget.Amount / DateTime.DaysInMonth(budgetYear, budgetMonth));
        }

        public List<BudgetViewModel> SelectBudgets(DateRangeService range, List<BudgetViewModel> budgets)
        {
            List<BudgetViewModel> selectedBudgets = new List<BudgetViewModel>();
            if (range.IsRangeInSameYearMonth())
            {
                var yearMonth = $"{range.SatrtDate.Split('-')[0]:0000}-{range.SatrtDate.Split('-')[1]:00}";
                var selectedBudget = (from a in budgets where a.Month.Equals(yearMonth) select a).FirstOrDefault();
                if (selectedBudget != null)
                {
                    selectedBudgets.Add(selectedBudget);
                }

                return selectedBudgets;
            }

            for (var idx = 0; idx < budgets.Count; idx++)
            {
                var budget = budgets[idx];
                var budgetYear = Convert.ToInt32(budget.Month.Split('-')[0]);
                var budgetMonth = Convert.ToInt32(budget.Month.Split('-')[1]);
                if (range.IsYearMonthInRange(budgetYear, budgetMonth))
                {
                    selectedBudgets.Add(budget);
                }
            }

            return selectedBudgets;
        }
    }

    public interface IBudgetService
    {
        void Add(BudgetViewModel budget);
        int CalculateTotalInRange(string startDate, string endDate, List<BudgetViewModel> budgets);
        int CalculateTotalInRange(DateRangeService range, List<BudgetViewModel> budgets);
        List<BudgetViewModel> SelectBudgets(DateRangeService range, List<BudgetViewModel> budgets);
    }
}