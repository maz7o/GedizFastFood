using System;
using TechTalk.SpecFlow;

namespace FastFood
{
    [Binding]
    public class TestSteps
    {
        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int values)
        {
            Console.WriteLine(values);
        }
        
        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            Console.WriteLine("Add Pressed");
        }
        
        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int output)
        {
            if (output == 120)
            {
                Console.WriteLine("correct");
            }
            else
            {
                Console.WriteLine("false");
            }

        }
    }
}
