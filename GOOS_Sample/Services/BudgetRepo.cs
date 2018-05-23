using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GOOS_Sample.Models;

namespace GOOS_Sample.Services
{
    public class BudgetRepo : IBudgetRepo
    {
        GooseSampleEntities _dbcontext;
        public Budget FindByMonth(string month)
        {
            using (_dbcontext = new GooseSampleEntities())
            {
                var budget = _dbcontext.Budgets.FirstOrDefault(x => x.YearMonth == month);
                return budget;
            }
        }

        public void Add(Budget model)
        {
            using (var _dbcontext = new GooseSampleEntities())
            {
                var budget = this.FindByMonth(model.YearMonth);
                if (budget == null)
                {
                    _dbcontext.Budgets.Add(model);
                }

                _dbcontext.SaveChanges();
            }
        }

        public IEnumerable<Budget> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}