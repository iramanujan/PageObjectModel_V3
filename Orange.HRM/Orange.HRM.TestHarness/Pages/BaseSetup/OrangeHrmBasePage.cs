using OpenQA.Selenium;
using Orange.HRM.Common.Configuration;
using Orange.HRM.Common.Handler.Browser;
using Orange.HRM.Common.Handler.HtmlElement;
using WebAutomation.Common.GenericHelper.ReportHandler;

namespace Orange.HRM.TestHarness.Pages.BaseSetup
{
    public class OrangeHrmBasePage
    {
        public Browser browser { get; }
        public IWebDriver webDriver { get; }
        public OrangeHrmBasePage(Browser browser)
        {
            this.browser = browser;
            this.webDriver = browser.webDriver;
        }

        public Report ObjReport => Report.ReportInstance;

        public readonly AppConfigMember appConfigMember = AppConfigReader.GetToolConfig();

        public HtmlElement htmlElement = new HtmlElement();
        
    }
}
