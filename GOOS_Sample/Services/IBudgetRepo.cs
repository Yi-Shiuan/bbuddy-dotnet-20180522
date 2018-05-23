using GOOS_Sample.Models;

namespace GOOS_Sample.Services
{
    public interface IBudgetRepo
    {
        Budget FindByMonth(string month);
        void Add(Budget model);

    }
}