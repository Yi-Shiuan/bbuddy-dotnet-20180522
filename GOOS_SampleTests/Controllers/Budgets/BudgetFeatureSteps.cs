using System;
using FluentAutomation;
using TechTalk.SpecFlow;

namespace GOOS_SampleTests
{
    [Binding]
    public class BudgetFeatureSteps : FluentTest
    {
        public BudgetFeatureSteps()
        {
            SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome);
        }

        [When(@"I add a buget (.*) for ""(.*)""")]
        public void WhenIAddABugetFor(int amount, string yearMonth)
        {
            I.Open("http://parallels-win10:58527/budgets/add")
                .Enter(amount.ToString()).In("#amount")
                .Enter(yearMonth).In("#yearMonth")
                .Click("input[type='submit']");
        }

        [Then(@"it should display (.*) for ""(.*)""")]
        public void ThenItShouldDisplayFor(int amount, string yearMonth)
        {
            I.Assert.Text(yearMonth).In($".{yearMonth}>td:nth-child(1)");
            I.Assert.Text(amount.ToString()).In($".{yearMonth}>td:nth-child(2)");
        }
    }
}
