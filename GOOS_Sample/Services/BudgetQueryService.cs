using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GOOS_Sample.Models;

namespace GOOS_Sample.Services
{
    public class BudgetQueryService
    {
        private List<BudgetViewModel> budgetData;
        public int GetTotalBudget(string start, string end)
        {
            InitData();
            DateTime startDateTime = DateTime.Parse(start);
            DateTime endDateTime = DateTime.Parse(end);
            
            var includedBudget =
                (from b in budgetData where IsBudgetInRange(b.Month, startDateTime, endDateTime) select b).ToList();
            var budgetSum = 0;
            for (var idx = 0; idx < includedBudget.Count(); idx++)
            {
                var curBudget = includedBudget[idx];
                var budgetYear = System.Convert.ToInt32(curBudget.Month.Split('-')[0]);
                var budgetMonth = System.Convert.ToInt32(curBudget.Month.Split('-')[1]);
                if (IsFullMonthIncluded(startDateTime, endDateTime, budgetYear, budgetMonth))
                {
                    budgetSum += curBudget.Amount;
                }
                else
                {
                    var dailyAvg = System.Convert.ToInt32(curBudget.Amount / DateTime.DaysInMonth(budgetYear, budgetMonth));
                    if (!IsDateInRange(startDateTime, endDateTime, new DateTime(budgetYear, budgetMonth, 1)))
                    {
                        // Use Start Date
                        var endOfMonth = new DateTime(budgetYear, budgetMonth, DateTime.DaysInMonth(budgetYear, budgetMonth));
                        var days = (endOfMonth - startDateTime).TotalDays + 1;
                        budgetSum += Convert.ToInt32(days * dailyAvg);
                    }
                    else
                    {
                        // Use EndDate
                        var beginningOfMonth = new DateTime(budgetYear, budgetMonth, 1);
                        var days = (endDateTime - beginningOfMonth).TotalDays + 1;
                        budgetSum += Convert.ToInt32(days * dailyAvg);
                    }
                }
            }

            return budgetSum;

        }

        private void InitData()
        {
            budgetData = new List<BudgetViewModel>
            {
                new BudgetViewModel {Month = "2018-05", Amount = 310},
                new BudgetViewModel {Month = "2018-06", Amount = 600},
                new BudgetViewModel {Month = "2018-07", Amount = 620}
            };
        }

        public bool IsBudgetInRange(string month, DateTime startDate, DateTime endDate)
        {
            var budgetYear = System.Convert.ToInt32(month.Split('-')[0]);
            var budgetMonth = System.Convert.ToInt32(month.Split('-')[1]);
            return (IsFullMonthIncluded(startDate, endDate, budgetYear, budgetMonth) || IsMonthStartIncluded(startDate, endDate, budgetYear, budgetMonth) || IsMonthEndIncluded(startDate, endDate, budgetYear, budgetMonth));
        }

        public bool IsFullMonthIncluded(DateTime startDate, DateTime endDate, int year, int month)
        {
            DateTime monthStartDate = new DateTime(year, month, 1);
            DateTime monthEndDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            return IsDateInRange(startDate, endDate, monthStartDate) && IsDateInRange(startDate, endDate, monthEndDate);
        }

        public bool IsMonthStartIncluded(DateTime startDate, DateTime endDate, int year, int month)
        {
            DateTime monthStartDate = new DateTime(year, month, 1);
            return IsDateInRange(startDate, endDate, monthStartDate);
        }

        public bool IsMonthEndIncluded(DateTime startDate, DateTime endDate, int year, int month)
        {
            DateTime monthEndDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            return IsDateInRange(startDate, endDate, monthEndDate);
        }

        public bool IsDateInRange(DateTime startDate, DateTime endDate, DateTime dateToCheck)
        {
            return dateToCheck >= startDate && dateToCheck < endDate;
        }

        public int GetBudgetOfTheMonth(int year, int month)
        {
            var dateStr = $"{year}-{month:00}";
            
            return (from a in budgetData where a.Month == dateStr select a).FirstOrDefault().Amount;
        }
    }
}