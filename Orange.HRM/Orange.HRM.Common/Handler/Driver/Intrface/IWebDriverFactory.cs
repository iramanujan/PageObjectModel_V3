using OpenQA.Selenium;

namespace Orange.HRM.Common.Handler.Driver.Intrface
{
    public interface IWebDriverFactory
    {
        IWebDriver InitializeWebDriver();
    }
}
