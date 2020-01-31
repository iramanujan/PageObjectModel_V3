using Orange.HRM.Common.Configuration;
using WebDriverHelper.DriverFactory;
namespace Orange.HRM.Common.Context
{
    public static class TestHarnessContextHelper
    {

        public static readonly AppConfigMember appConfigMember = AppConfigReader.GetToolConfig();

        public static TestHarnessContext CreateDefault()
        {
            return new TestHarnessContext();
        }

        public static TestHarnessContext CreateLocalDriverContext()
        {
            var factory = WebDriverType.Get(appConfigMember.Browser, AppConfigMember.WebDriverExecutionType.Local).Factory;
            return new TestHarnessContext(factory);
        }
    }
}
