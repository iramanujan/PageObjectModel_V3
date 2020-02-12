using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Orange.HRM.Common.Configuration;
using Orange.HRM.Common.Handler.Browser;
using Orange.HRM.TestHarness.Pages.Qualifications;
using Orange.HRM.TestHarness.Steps.Context;
using Orange.HRM.TestHarness.Steps.Login;
using WebAutomation.Common.GenericHelper.ReportHandler;

namespace Orange.HRM.Test
{
    public class OrangeHrmBaseTest
    {
        public readonly AppConfigMember appConfigMember = AppConfigReader.GetToolConfig();
        public TestHarnessContext myContext { get; } = TestHarnessContextHelper.CreateDefault();

        public Browser browser => myContext.Browser;
        public Report ObjReport => Report.ReportInstance;

        public LoginStep loginStep => myContext.GetOrangeHrmSteps<LoginStep>();
        public EducationStep educationStep => myContext.GetOrangeHrmSteps<EducationStep>();

        [TearDown]
        public void OrangeHrmBaseTestOneTimeTearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                ObjReport.Error(TestContext.CurrentContext.Test.MethodName);
            }
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Warning)
            {
                ObjReport.Warning(TestContext.CurrentContext.Test.MethodName);
            }
            myContext.Browser.CloseAllBrosr();
        }

    }
}
