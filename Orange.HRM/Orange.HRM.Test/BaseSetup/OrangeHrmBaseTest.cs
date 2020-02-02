using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Orange.HRM.Common.Configuration;
using Orange.HRM.Common.Context;
using Orange.HRM.Common.Helper;
using System;
using WebAutomation.Common.GenericHelper.ReportHandler;

namespace Orange.HRM.Test.BaseSetup
{
    public class OrangeHrmBaseTest
    {
        public readonly AppConfigMember appConfigMember = AppConfigReader.GetToolConfig();
        protected TestHarnessContext myContext { get; } = TestHarnessContextHelper.CreateDefault();
        public Report ObjReport => Report.ReportInstance;

        protected void ExecuteSafely(Action steps)
        {
            StepsExecutor.ExecuteSafely(steps);
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            ExecuteSafely(myContext.Browser.CreateUploadDwonloadDirectory);
        }

        [SetUp]
        public void OrangeHrmBaseTestOneTimeSetUp()
        {
            //ExecuteSafely(myContext.Browser.CreateUploadDwonloadDirectory);
            myContext.Browser.InitBrowser(appConfigMember.Url);
            ObjReport.CreateTest(TestContext.CurrentContext.Test.Name);
        }

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
        
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            //ExecuteSafely(myContext.Browser.CleanupCreatedDirectoriesSafely);
            ObjReport.ExtentReportsTearDown();
        }

    }
}
