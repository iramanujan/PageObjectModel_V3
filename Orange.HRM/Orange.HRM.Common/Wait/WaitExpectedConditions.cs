using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Orange.HRM.Common.Configuration;
using Orange.HRM.Common.Handler.Browser;
using System;
using WebAutomation.Common.Wait;

namespace Orange.HRM.Common.Wait
{

    public sealed class WaitExpectedConditions
    {
        private static readonly AppConfigMember appConfigMember = AppConfigReader.GetToolConfig();
        private IWebDriver webDriver = null;
        private DefaultWait<IWebDriver> fluentWait = null;

        public WaitExpectedConditions() : this(BrowserContext.browser.webDriver)
        {
            this.fluentWait = new DefaultWait<IWebDriver>(this.webDriver);
            this.fluentWait.Timeout = TimeSpan.FromSeconds(appConfigMember.ObjectTimeout);
            this.fluentWait.PollingInterval = TimeSpan.FromMilliseconds(appConfigMember.PollingInterval);
            //this.fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException), typeof(WebDriverTimeoutException));
        }
        public WaitExpectedConditions(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public IWebDriver WaitTillSwitchToFrameByIndex(int frameIndex)
        {
            return this.fluentWait.Until<IWebDriver>(webDriver => webDriver.SwitchTo().Frame(frameIndex));
        }
        public IWebDriver WaitTillSwitchToFrameByFrameName(string frameName)
        {
            return this.fluentWait.Until<IWebDriver>(webDriver => webDriver.SwitchTo().Frame(frameName));
        }
        public IWebDriver WaitTillSwitchToFrameByWebElement(IWebElement webElement)
        {
            return this.fluentWait.Until<IWebDriver>(webDriver => webDriver.SwitchTo().Frame(webElement));
        }
        public IAlert WaitTillSwitchToAlert()
        {
            return this.fluentWait.Until<IAlert>(webDriver => webDriver.SwitchTo().Alert());
        }
        public void WaitTillAcceptAlert(IAlert alert)
        {
            Waiter.SpinWaitEnsureSatisfied(() =>
            {
                try
                {
                    alert.Accept();
                    return true;
                }
                catch (NoAlertPresentException)
                {
                    return false;
                }
            }, "Alert Dialog Still Presents", timeOutSeconds: appConfigMember.ObjectTimeout, pollIntervalSeconds: appConfigMember.PollingInterval);

        }
        public void WaitTillAcceptAlert(IAlert alert, string value)
        {
            Waiter.SpinWaitEnsureSatisfied(() =>
            {
                try
                {
                    alert.SendKeys(value);
                    alert.Accept();
                    return true;
                }
                catch (NoAlertPresentException)
                {
                    return false;
                }
            }, "Alert Dialog Still Presents", timeOutSeconds: appConfigMember.ObjectTimeout, pollIntervalSeconds: appConfigMember.PollingInterval);

        }
        public void WaitTillDismissAlert(IAlert alert)
        {
            Waiter.SpinWaitEnsureSatisfied(() =>
            {
                try
                {
                    alert.Dismiss();
                    return true;
                }
                catch (NoAlertPresentException)
                {
                    return false;
                }
            }, "Alert Dialog Still Presents", timeOutSeconds: appConfigMember.ObjectTimeout, pollIntervalSeconds: appConfigMember.PollingInterval);

        }
        public void WaitTillDismissAlert(IAlert alert, string value)
        {
            Waiter.SpinWaitEnsureSatisfied(() =>
            {
                try
                {
                    alert.SendKeys(value);
                    alert.Dismiss();
                    return true;
                }
                catch (NoAlertPresentException)
                {
                    return false;
                }
            }, "Alert Dialog Still Presents", timeOutSeconds: appConfigMember.ObjectTimeout, pollIntervalSeconds: appConfigMember.PollingInterval);
        }
        public IWebDriver WaitTillSwitchToWindowHandle(string windowName)
        {
            return this.fluentWait.Until<IWebDriver>(webDriver => webDriver.SwitchTo().Window(windowName));
        }

    }
}
