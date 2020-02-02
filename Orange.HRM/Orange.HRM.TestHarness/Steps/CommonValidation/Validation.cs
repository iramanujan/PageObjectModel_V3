using Orange.HRM.TestHarness.Steps.BaseSetup;
using System;
using WebAutomation.Common.Wait;

namespace Orange.HRM.TestHarness.Steps.CommonValidation
{
    public class Validation : OrangeHrmBaseSteps
    {
    
        public void VerifyPageText(string pageSource)
        {
            Waiter.SpinWaitEnsureSatisfied(
                () => webDriver.PageSource.ToLower().Contains(pageSource.ToLower()), 
                TimeSpan.FromSeconds(60), 
                TimeSpan.FromSeconds(3),
                $"Page Source Match : Page Source '{browser.GetDecodedUrl()}' contain \"{pageSource}\".",
                $"Page Source Mismatch : Page Source '{browser.GetDecodedUrl()}' doesn't contain \"{pageSource}\"."
             );

        }

        public void VerifyPageUrl(string pageUrl)
        {
            Waiter.SpinWaitEnsureSatisfied(
                  () => webDriver.Url.ToLower().Contains(pageUrl.ToLower()),
                  TimeSpan.FromSeconds(60),
                  TimeSpan.FromSeconds(3),
                  $"URL Match : Url '{browser.webDriver.Url}' contain \"{pageUrl}\".",
                  $"URL Mismatch : Url '{browser.GetDecodedUrl()}' doesn't contain \"{pageUrl}\"."
               );
        }
    }
}
