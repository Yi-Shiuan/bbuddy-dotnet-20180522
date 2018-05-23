using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOOS_Sample.Services
{
    public class DateRangeService
    {
        public string SatrtDate { get; set; }
        public string EndDate { get; set; }

        public DateTime StartDateTime()
        {
            return DateTime.Parse(SatrtDate);
        }

        public DateTime EndDateTime()
        {
            return DateTime.Parse(EndDate);

        }
        
        public bool IsYearMonthInRange(int year, int month)
        {
            return true;
        }

        public bool IsRangeInSameYearMonth()
        {
            DateTime sDate = DateTime.Parse(SatrtDate);
            DateTime eDate = DateTime.Parse(EndDate);
            return (sDate.Year == eDate.Year) && (sDate.Month == eDate.Month);
        }

        public bool IsYearMonthFullInRange(int year, int month)
        {
            var FirstDayOfMonth = new DateTime(year, month,1);
            var LastDayOfMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            return IsDateInRange(FirstDayOfMonth) && IsDateInRange(LastDayOfMonth);
        }

        public bool IsYearMonthOnFirstMonthOfRange(int year, int month)
        {
            var FirstDayOfMonth = new DateTime(year, month,1);
            var LastDayOfMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            return !IsDateInRange(FirstDayOfMonth) && IsDateInRange(LastDayOfMonth); 
        }
        public bool IsYearMonthOnLastMonthOfRange(int year, int month)
        {
            var FirstDayOfMonth = new DateTime(year, month,1);
            var LastDayOfMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            return IsDateInRange(FirstDayOfMonth) && !IsDateInRange(LastDayOfMonth); 
        }

        public bool IsDateInRange(DateTime dateToCheck)
        {
            return DateTime.Parse(SatrtDate) <= dateToCheck && DateTime.Parse(EndDate) >= dateToCheck;
        }





    }
}