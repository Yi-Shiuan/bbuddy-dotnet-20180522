using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAutomation;

namespace GOOS_SampleTests.Pages
{
    internal class BudgetCreatePage : PageObject<BudgetCreatePage>
    {
        public BudgetCreatePage(FluentTest test) : base(test)
        {
            //this.Url = $"{PageContext.Domain}/budget/add";
        }
    }
}
