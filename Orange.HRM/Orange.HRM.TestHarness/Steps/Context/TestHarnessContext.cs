using Orange.HRM.Common.Configuration;
using Orange.HRM.Common.Handler.Browser;
using Orange.HRM.Common.Handler.Driver.Intrface;
using Orange.HRM.TestHarness.Steps.BaseSetup;
using Orange.HRM.TestHarness.Utils;
using WebDriverHelper.DriverFactory;

namespace Orange.HRM.TestHarness.Steps.Context
{
    public class TestHarnessContext
    {
        public static readonly AppConfigMember appConfigMember = AppConfigReader.GetToolConfig();
        public Browser Browser { get; }

        public TestHarnessContext(IWebDriverFactory factory)
        {
            if(this.Browser == null)
            {
                this.Browser = new Browser(factory);
                if (BrowserContext.browser == null)
                {
                    BrowserContext.browser = this.Browser;
                }
            }
        }

        public TestHarnessContext() : this(WebDriverType.Get(appConfigMember.Browser, appConfigMember.ExecutionType).Factory)
        {
        }

        public OrangeHrmSteps GetOrangeHrmSteps<OrangeHrmSteps>(params object[] additionalArgs) where OrangeHrmSteps : OrangeHrmBaseSteps
        {
            return CreateObjectUtils.CreateOrangeHrmSteps<OrangeHrmSteps>(Browser, additionalArgs);
        }

    }
}
