using OpenQA.Selenium;
using Orange.HRM.Common.Configuration;
using Orange.HRM.Common.Context;
using Orange.HRM.Common.Handler.Browser;
using WebAutomation.Common.GenericHelper.ReportHandler;

namespace Orange.HRM.TestHarness.Pages.BaseSetup
{
    public class OrangeHrmBasePage
    {
        public Report ObjReport => Report.ReportInstance;

        public readonly AppConfigMember appConfigMember = AppConfigReader.GetToolConfig();
        public Browser browser => BrowserContext.browser;

        public IWebDriver webDriver => BrowserContext.browser.webDriver;
    }
}
