using OpenQA.Selenium;
using Orange.HRM.Common.Configuration;
using Orange.HRM.Common.Context;
using Orange.HRM.Common.Handler.Browser;
using WebAutomation.Common.GenericHelper.ReportHandler;

namespace Orange.HRM.TestHarness.Steps.BaseSetup
{
    public class OrangeHrmBaseSteps
    {
        public readonly AppConfigMember appConfigMember = AppConfigReader.GetToolConfig();
        public Report ObjReport => Report.ReportInstance;
        public Browser browser => BrowserContext.browser;

        public IWebDriver webDriver = BrowserContext.browser.webDriver;
    }
}
