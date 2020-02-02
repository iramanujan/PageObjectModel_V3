using OpenQA.Selenium;
using Orange.HRM.Common.Configuration;
using Orange.HRM.Common.Context;
using System;
using System.Linq;
using WebAutomation.Common.Wait;

namespace Orange.HRM.Common.Handler.HtmlElement.Element
{
    public class HtmlTextBox
    {
        public static readonly AppConfigMember appConfigMember = AppConfigReader.GetToolConfig();
        public IWebDriver webDriver = null;

        public HtmlTextBox(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public HtmlTextBox() : this(BrowserContext.browser.webDriver)
        {

        }
        
        public string Text(IWebElement webElement)
        {
            try
            {
                string textContentValue = webElement.GetAttribute(TagAttributes.TextContent);
                if (!string.IsNullOrEmpty(textContentValue))
                {
                    return textContentValue;
                }
            }
            catch (StaleElementReferenceException)
            {
                try
                {
                    string value = webElement.GetAttribute(TagAttributes.Value);
                    if (!string.IsNullOrEmpty(value))
                    {
                        return value;
                    }
                }
                catch (StaleElementReferenceException)
                {

                }
            }

            return webElement.Text;
        }

        public string TrimText(IWebElement webElement)
        {
            return webElement.Text.Trim();
        }

        public void WaitForTextEntered(IWebElement webElement)
        {
            Waiter.SpinWaitEnsureSatisfied(() => webElement.Text.Length > 0,
                TimeSpan.FromSeconds(appConfigMember.ObjectTimeout),
                TimeSpan.FromSeconds(appConfigMember.PollingInterval),
                $"Text Element is still empty");
        }

        public void SetTextAndClickEnter(IWebElement webElement, string value)
        {
            webElement.SendKeys(value);
            webElement.SendKeys(Keys.Enter);
        }

        public void EnterSetTextAndSubmit(IWebElement webElement, string value)
        {
            ClearUsingKeyAndSendKeysWithSubmit(webElement, value, Keys.Enter);
        }

        private void ClearUsingKeyAndSendKeys(IWebElement webElement, string value, string clearKey)
        {
            Enumerable.Range(0, webElement.Text.Length).ToList().ForEach(arg => webElement.SendKeys(clearKey));

            if (webElement.Text.Length > 0)
            {
                webElement.Clear();
            }

            webElement.SendKeys(value);
        }

        public void ClearUsingKeyAndSendKeysWithSubmit(IWebElement webElement, string value, string clearKey)
        {
            Enumerable.Range(0, webElement.Text.Length).ToList().ForEach(arg => webElement.SendKeys(clearKey));

            if (webElement.Text.Length > 0)
            {
                webElement.Clear();
            }
            webElement.SendKeys(value);
            webElement.SendKeys(Keys.Tab);
        }

        public void Clear(IWebElement webElement)
        {
            webElement.Clear();
        }

        public void ClearUsingBackspace(IWebElement webElement)
        {
            Enumerable.Range(0, webElement.Text.Length).ToList().ForEach(arg => webElement.SendKeys(Keys.Backspace));
        }

        public void SendKeys(IWebElement webElement, string value)
        {
            webElement.SendKeys(value);
        }
        
        public void ClearAndSendKeys(IWebElement webElement, string value)
        {
            webElement.Clear();
            webElement.SendKeys(value);
        }

        public void ClearUsingBackspaceAndSendKeys(IWebElement webElement, string value)
        {
            ClearUsingKeyAndSendKeys(webElement, value, Keys.Backspace);
        }
        
        public void ClearUsingDeleteAndSendKeys(IWebElement webElement, string value)
        {
            ClearUsingKeyAndSendKeys(webElement, value, Keys.Delete);
        }

        public void UnhideAndUploadFile(string keys)
        {
            //string js = " arguments[0].classList.remove(\"hidden\")";
            //Browser.ExecuteScript(js, this.OriginalWebElement);
            //this.WaitForDisplayed();
            //Thread.Sleep(1000);
            //WrappedElement.SendKeys(keys);
        }

    }
}
