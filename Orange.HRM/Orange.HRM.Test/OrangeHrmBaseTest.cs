using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Orange.HRM.Common.Configuration;
using Orange.HRM.TestHarness.Steps.Context;
using WebAutomation.Common.GenericHelper.ReportHandler;

namespace Orange.HRM.Test
{
    public class OrangeHrmBaseTest
    {
        public readonly AppConfigMember appConfigMember = AppConfigReader.GetToolConfig();
        protected TestHarnessContext myContext { get; } = TestHarnessContextHelper.CreateDefault();
        public Report ObjReport => Report.ReportInstance;

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
            //ExecuteSafely(myContext.Browser.CleanupCreatedDirectoriesSafely);
            myContext.Browser.CloseAllBrosr();
        }

    }
}
