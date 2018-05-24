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

        public int CalculateRangeTotal(string startDate, string endDate, List<BudgetViewModel> budgets)
        {
            return CalculateRangeTotal(new DateRangeService {SatrtDate = startDate, EndDate = endDate}, budgets);
        }

        public int CalculateRangeTotal(DateRangeService range, List<BudgetViewModel> budgets)
        {
            var total = 0;
            if (range.IsRangeInSameYearMonth())
            {
                var budget = SelectBudgetByYearMonth(range.StartDateTime().Year, range.StartDateTime().Month, budgets);
                total = (budget == null)
                    ? 0
                    : CalculatePartialMontnBudget(range.StartDateTime(), range.EndDateTime(), budget);
                return total;
            }

            for (var idx = 0; idx < budgets.Count; idx++)
            {
                var budget = budgets[idx];
                var budgetYear = Convert.ToInt32(budget.Month.Split('-')[0]);
                var budgetMonth = Convert.ToInt32(budget.Month.Split('-')[1]);
                if (range.IsYearMonthInRange(budgetYear, budgetMonth))
                {
                    if (range.IsYearMonthFullInRange(budgetYear, budgetMonth))
                    {
                        total += budget.Amount;
                    }
                    else if (range.IsYearMonthOnFirstMonthOfRange(budgetYear, budgetMonth))
                    {
                        total += CalculatePartialMontnBudget(range.StartDateTime(),
                            new DateTime(budgetYear, budgetMonth, DateTime.DaysInMonth(budgetYear, budgetMonth)),
                            budget);
                    }
                    else if (range.IsYearMonthOnLastMonthOfRange(budgetYear, budgetMonth))
                    {
                        total += CalculatePartialMontnBudget(new DateTime(budgetYear, budgetMonth, 1),
                            range.EndDateTime(), budget);
                    }
                }
            }

            return total;
        }

        private int CalculatePartialMontnBudget(DateTime startDateTime, DateTime endDateTime, BudgetViewModel budget)
        {
            var days = (endDateTime - startDateTime).Days + 1;
            return days * CalculateAverageBudget(budget);
        }

        public int CalculateAverageBudget(BudgetViewModel budget)
        {
            var budgetYear = Convert.ToInt32(budget.Month.Split('-')[0]);
            var budgetMonth = Convert.ToInt32(budget.Month.Split('-')[1]);
            return Convert.ToInt32(budget.Amount / DateTime.DaysInMonth(budgetYear, budgetMonth));
        }

        public BudgetViewModel SelectBudgetByYearMonth(int year, int month, List<BudgetViewModel> budgets)
        {
            var condition = $"{year:0000}-{month:00}";
            for (var idx = 0; idx < budgets.Count; idx++)
            {
                if (budgets[idx].Month.Equals(condition))
                {
                    return budgets[idx];
                }
            }
            return null;
        }
    }

    public interface IBudgetService
    {
        void Add(BudgetViewModel budget);
        int CalculateRangeTotal(string startDate, string endDate, List<BudgetViewModel> budgets);
        int CalculateRangeTotal(DateRangeService range, List<BudgetViewModel> budgets);
        //List<BudgetViewModel> SelectBudgets(DateRangeService range, List<BudgetViewModel> budgets);
    }
}