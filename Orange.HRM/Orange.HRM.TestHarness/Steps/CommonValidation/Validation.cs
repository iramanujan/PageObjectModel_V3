using OpenQA.Selenium;
using Orange.HRM.Common.Handler.Browser;
using Orange.HRM.TestHarness.Steps.BaseSetup;
using System;
using WebAutomation.Common.Wait;

namespace Orange.HRM.TestHarness.Steps.CommonValidation
{
    public class Validation
    {

        private Browser browser = null;
        private IWebDriver webDriver = null;
        public Validation(Browser browser)
        {
            this.browser = browser;
            this.webDriver = browser.webDriver;
        }
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

        public void VerifyText(string expectedText, string actuslText,string info)
        {
            string msg = "Actual Text: " + actuslText + "\n Expected Text: " + expectedText;
            if (expectedText.Trim().ToLower().Equals(actuslText.Trim().ToLower()))
            {
                ObjReport.Pass(info, msg);
            }
            else
            {
                ObjReport.Error(info, msg);
            }

        }
    }
}
