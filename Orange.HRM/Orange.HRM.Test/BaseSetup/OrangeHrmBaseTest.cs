using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Orange.HRM.Common.Configuration;
using Orange.HRM.Common.Context;
using Orange.HRM.Common.Helper;
using System;
using WebAutomation.Common.GenericHelper.ReportHandler;
using static Orange.HRM.Common.Configuration.AppConfigMember;

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
            
        }

        [SetUp]
        public void OrangeHrmBaseTestOneTimeSetUp()
        {
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
            ExecuteSafely(myContext.Browser.CleanupCreatedDirectoriesSafely);
            ExecuteSafely(myContext.Browser.Quit);
            switch (appConfigMember.Browser)
            {
                case BrowserType.IE:
                    //Killing IE driver process if exists
                    ProcessUtils.KillProcesses("iexplore");
                    ProcessUtils.KillProcesses("IEDriverServer");
                    break;
                case BrowserType.Chrome:
                    ProcessUtils.KillProcesses("chrome");
                    ProcessUtils.KillProcesses("chromedriver");
                    ProcessUtils.KillProcesses("chrome.exe");
                    ProcessUtils.KillProcesses("chromedriver.exe");
                    break;
                case BrowserType.Firefox:
                    ProcessUtils.KillProcesses("firefox.exe");
                    ProcessUtils.KillProcesses("geckodriver.exe");
                    ProcessUtils.KillProcesses("firefox");
                    ProcessUtils.KillProcesses("geckodriver");
                    break;
            }
        }
        
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ExecuteSafely(myContext.Browser.CleanupCreatedDirectoriesSafely);
            ObjReport.ExtentReportsTearDown();
        }

    }
}
