using System;
using TechTalk.SpecFlow;
using FluentAutomation;

namespace GOOS_SampleTests.Steps
{
    [Binding]
    public class BudgetsSteps : FluentTest
    {
        public BudgetsSteps()
        {
         //SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome );
        }

        [When(@"I add buget for ""(.*)"" with amount (.*)")]
        public void WhenIAddBugetForWithAmount(string month, int amount)
        {
            //I.Open("http://www.google.com");
            var domain = "http://localhost:58527";
            I.Open($"{domain}/budgets/add");
            I.Enter(month).In("#month");
            I.Enter(amount.ToString()).In("#amount");
            I.Click("#save");
        }

        [Then(@"I will see the following list")]
        public void ThenIWillSeeTheFollowingList(Table table)
        {
        }
    }
}