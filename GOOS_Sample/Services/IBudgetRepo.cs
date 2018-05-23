using System.Collections.Generic;
using GOOS_Sample.Models;

namespace GOOS_Sample.Services
{
    public interface IBudgetRepo
    {
        void Save(Budget budget);
        IEnumerable<Budget> GetBudgets();
        Budget GetBudgets(string v);
    }

    public class BudgetRepo : IBudgetRepo
    {
        private readonly ApplicationDbContext db;

        public BudgetRepo(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Save(Budget budget)
        {
            db.Budgets.Add(budget);
            db.SaveChanges();
        }

        public IEnumerable<Budget> GetBudgets()
        {
            return db.Budgets;
        }

        public Budget GetBudgets(string v)
        {
            throw new System.NotImplementedException();
        }
    }
}