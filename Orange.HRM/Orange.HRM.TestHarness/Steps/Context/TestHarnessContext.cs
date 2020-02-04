using Orange.HRM.Common.Configuration;
using Orange.HRM.Common.Handler.Browser;
using Orange.HRM.Common.Handler.Driver.Intrface;
using Orange.HRM.TestHarness.Steps.BaseSetup;
using WebDriverHelper.DriverFactory;

namespace Orange.HRM.TestHarness.Steps.Context
{
    public class TestHarnessContext
    {
        public static readonly AppConfigMember appConfigMember = AppConfigReader.GetToolConfig();
        public Browser Browser { get; }

        public TestHarnessContext(IWebDriverFactory factory)
        {
            this.Browser = new Browser(factory);
            if (BrowserContext.browser == null)
            {
                BrowserContext.browser = this.Browser;
            }
        }

        public OrangeHrmBaseSteps GetOrangeHrmBaseSteps()
        {
            return new OrangeHrmBaseSteps(Browser);
        }

        public TestHarnessContext() : this(WebDriverType.Get(appConfigMember.Browser, appConfigMember.ExecutionType).Factory)
        {
        }

    }
}
