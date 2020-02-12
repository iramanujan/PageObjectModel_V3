using Orange.HRM.Common.Handler.Browser;
using Orange.HRM.TestHarness.Pages.BaseSetup;

namespace Orange.HRM.TestHarness.Pages.Dashboard
{
    public class DashboardPage : OrangeHrmBasePage
    {
        public DashboardPage(Browser browser):base(browser)
        {

        }
        internal readonly string pageSource = "Leave Entitlements and Usage Report";

        internal readonly string pageUrl = "/index.php/dashboard";
    }
}
