using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Orange.HRM.Common.Configuration;
using Orange.HRM.Common.Context;
using Orange.HRM.Common.Handler.Browser;
using Orange.HRM.Common.Handler.Log;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WebAutomation.Common.GenericHelper.ReportHandler;
using WebAutomation.Common.Wait;

namespace Orange.HRM.Common.Handler.HtmlElement
{
    public enum How
    {
        [Description("XPath")]
        XPath = 0,

        [Description("CssSelector")]
        CssSelector = 1,
    }
    public class HtmlElement
    {
        public static readonly AppConfigMember appConfigMember = AppConfigReader.GetToolConfig();
        public Report ObjReport => Report.ReportInstance;
        public IWebDriver webDriver = BrowserContext.browser.webDriver;
        private const string ERROR_ELEMENT_NOT_FOUND_MESSAGE_FORMAT = "Warning : Element not found on page url: {0}{1}element locator: {2}";
        private DefaultWait<IWebDriver> fluentWait = null;
        private WebDriverWait webDriverWait = null;
        private DefaultWait<IWebElement> webElementFluentWait = null;
        private JavaScript javaScript = null;

        public HtmlElement(IWebDriver webDriver, int Timeout, int PollingInterval)
        {
            this.webDriver = webDriver;
            this.javaScript = new JavaScript();
            this.webDriverWait = Waiter.Wait(webDriver, appConfigMember.ObjectTimeout);
            this.fluentWait = new DefaultWait<IWebDriver>(this.webDriver);
            this.fluentWait.Timeout = TimeSpan.FromSeconds(appConfigMember.ObjectTimeout);
            this.fluentWait.PollingInterval = TimeSpan.FromMilliseconds(appConfigMember.PollingInterval);
            this.fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException), typeof(WebDriverTimeoutException));
        }

        public HtmlElement(IWebDriver webDriver) : this(webDriver, appConfigMember.ObjectTimeout, appConfigMember.PollingInterval)
        {
        }

        public HtmlElement() : this(BrowserContext.browser.webDriver, appConfigMember.ObjectTimeout, appConfigMember.PollingInterval)
        {

        }

        public By ByLocator(How findBy, string locator)
        {
            By by = null;
            if (findBy.Equals(How.XPath))
            {
                by = By.XPath(locator);
            }
            if (findBy.Equals(How.CssSelector))
            {
                by = By.CssSelector(locator);
            }
            return by;
        }

        public int GetColumnIndex(ReadOnlyCollection<IWebElement> TableHeader, string ColumnName, int ColumnIndex = 0, string Attribute = TagAttributes.Value)
        {
            foreach (var item in TableHeader)
            {
                try
                {
                    if (item.GetAttribute(Attribute).Trim().ToLower() == ColumnName.Trim().ToLower()) break;
                }
                catch
                {
                    try
                    {
                        if (item.Text.Trim().ToLower() == ColumnName.Trim().ToLower()) break;
                    }
                    catch (Exception ObjException2)
                    {
                        Logger.Error("Error Occured At Column" + ObjException2.Message);
                    }
                }
                ColumnIndex = ColumnIndex + 1;
            }
            return ColumnIndex;
        }

        public string GetAttribute(IWebElement webElement, string attribute = TagAttributes.Text)
        {
            if (webElement != null)
            {
                if (attribute.Contains(TagAttributes.Text))
                    return webElement.Text;
                else if (attribute.Contains(TagAttributes.Class))
                    return webElement.GetAttribute(TagAttributes.Class);
                else
                    return webElement.GetAttribute(attribute);
            }
            throw new NoSuchElementException("");
        }

        public Dictionary<string, Object> GetAllAttribute(IWebDriver webDriver, IWebElement webElement)
        {

            return javaScript.GetAttributes(JScriptType.GetAllAttribute, webDriver, webElement);
        }

        private string GetElementNotFoundMessage(string locator)
        {
            return String.Format(ERROR_ELEMENT_NOT_FOUND_MESSAGE_FORMAT, this.webDriver.Url, Environment.NewLine, locator);
        }

        public string GetText(How findBy, string locator)
        {
            By by = ByLocator(findBy, locator);
            IWebElement webElement = webDriverWait.Until(webDriver => webDriver.FindElement(by));
            if (webElement != null)
            {
                return webElement.Text;
            }
            else
            {
                throw new NoSuchElementException(GetElementNotFoundMessage("How:" + findBy.ToString() + ", Value: " + locator));
            }
        }

        public bool WaitTillWebElementDisplayed(By by)
        {
            return this.fluentWait.Until<Boolean>(webDriver => webDriver.FindElement(by).Displayed);
        }

        public bool WaitTillWebElementEnabled(By by)
        {
            return this.fluentWait.Until<Boolean>(webDriver => webDriver.FindElement(by).Enabled);
        }

        public bool WaitForWebElement(By by)
        {
            return this.fluentWait.Until<Boolean>(webDriver =>
                                                                    webDriver.FindElement(by).Enabled
                                                                & webDriver.FindElement(by).Displayed
                                                                & webDriver.FindElement(by).Size.Width > 0
                                                                & webDriver.FindElement(by).Size.Height > 0);
        }

        public IWebElement GetWebElement(By by)
        {
            IWebElement webElement = null;
            webElement = this.webDriver.FindElement(by);
            this.webElementFluentWait = new DefaultWait<IWebElement>(webElement);
            this.webElementFluentWait.Timeout = TimeSpan.FromSeconds(appConfigMember.ObjectTimeout);
            this.webElementFluentWait.PollingInterval = TimeSpan.FromMilliseconds(appConfigMember.PollingInterval);
            this.webElementFluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            Func<IWebElement, IWebElement> waiter = new Func<IWebElement, IWebElement>((IWebElement ele) =>
            {
                if (webElement.Enabled & webElement.Displayed & webDriver.FindElement(by).Size.Width > 0 & webDriver.FindElement(by).Size.Height > 0)
                {
                    return webElement;
                }
                else
                {
                    return null;
                }
            });
            this.webElementFluentWait.Until(waiter);
            return webElement;
        }

        public bool WaitForObjectNotDisplayed(By by)
        {
            return this.fluentWait.Until<Boolean>(webDriver => !webDriver.FindElement(by).Displayed);
        }

        public bool WaitForObjectNotEnabled(By by)
        {
            return this.fluentWait.Until<Boolean>(webDriver => !webDriver.FindElement(by).Enabled);
        }

        public bool WaitForObjectDisAppearance(By by)
        {
            return this.fluentWait.Until<Boolean>(webDriver => !webDriver.FindElement(by).Enabled & !webDriver.FindElement(by).Displayed);
        }

        public IWebElement WaitForProperty(By by, string value, string attribute = TagAttributes.Text)
        {
            IWebElement webElement = null;
            webElement = this.webDriver.FindElement(by);
            this.webElementFluentWait = new DefaultWait<IWebElement>(webElement);
            this.webElementFluentWait.Timeout = TimeSpan.FromSeconds(appConfigMember.ObjectTimeout);
            this.webElementFluentWait.PollingInterval = TimeSpan.FromMilliseconds(appConfigMember.PollingInterval);
            this.webElementFluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            Func<IWebElement, IWebElement> waiter = new Func<IWebElement, IWebElement>((IWebElement ele) =>
            {
                if (webElement.GetAttribute(attribute).Contains(value))
                {
                    return webElement;
                }
                else
                {
                    return null;
                }
            });
            this.webElementFluentWait.Until(waiter);
            return webElement;
        }

        public string GetAttributeOfNthElement(By by, int elementIndex, string attribute)
        {
            IWebElement webElement = this.webDriver.FindElements(by)[elementIndex];
            return GetAttribute(webElement, attribute);
        }
        public string GetCssValueOfNthElement(By by, int elementIndex, string cssValue)
        {
            return this.webDriver.FindElements(by)[elementIndex].GetCssValue(cssValue);
        }
        public object GetElement(How findBy, string locator)
        {
            try
            {
                return this.webDriver.FindElement(ByLocator(findBy, locator));
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        public object GetNthElement(By by, int elementIndex)
        {
            return this.webDriver.FindElements(by)[elementIndex];
        }

    }
}
