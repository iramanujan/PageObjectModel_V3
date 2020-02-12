using OpenQA.Selenium;
using Orange.HRM.Common.Handler.Browser;
using Orange.HRM.TestHarness.Pages.BaseSetup;
using System;

namespace Orange.HRM.TestHarness.Pages.Login
{
    public class LoginPage : OrangeHrmBasePage
    {
        public LoginPage(Browser browser) : base(browser)
        {

        }
        internal readonly string pageSource = "( Username : Admin | Password : admin123 )";

        internal readonly string pageUrl = @"Https://Opensource-Demo.Orangehrmlive.Com/";
        internal IWebElement UserName => webDriver.FindElement(By.CssSelector("#txtUsername"));
        internal IWebElement Password => webDriver.FindElement(By.CssSelector("#txtPassword"));
        internal IWebElement Login => webDriver.FindElement(By.CssSelector("#btnLogin"));
        internal IWebElement Message => webDriver.FindElement(By.CssSelector("#spanMessage"));
        internal String ErrorMessage => Message.Text.Trim();
    }
}
