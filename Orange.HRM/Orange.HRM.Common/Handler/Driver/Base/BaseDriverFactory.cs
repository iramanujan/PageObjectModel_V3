using Orange.HRM.Common.Configuration;
using OpenQA.Selenium;

namespace Orange.HRM.Common.Handler.Driver.Base
{
    public abstract class BaseDriverFactory
    {
        private readonly AppConfigMember appConfigMember = AppConfigReader.GetToolConfig();
        public abstract IWebDriver InitializeWebDriver();

        public IWebDriver WebDriverSetupSetps()
        {
            return InitializeWebDriver();
        }
    }
}
