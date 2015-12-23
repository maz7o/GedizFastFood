using System;
using TechTalk.SpecFlow;

namespace FastFood
{
    [Binding]
    public class SpecFlowFeaturefsdfsdfsSteps
    {
        [Given(@"I have entered (.*) into the field")]
        public void GivenIHaveEnteredIntoTheField(int x)
        {
      
        }
        
        [When(@"I press button")]
        public void WhenIPressButton()
        {
            
        }
        
        [Then(@"the result should be (.*) on the field")]
        public void ThenTheResultShouldBeOnTheField(int result)
        {
            if (result == 53)
                Console.WriteLine("true");
            else
                Console.WriteLine("false");
        }

    }
}
