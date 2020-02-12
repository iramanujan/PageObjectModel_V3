using NUnit.Framework;
using Orange.HRM.Common.Helper;
using System;
using WebAutomation.Common.GenericHelper.ReportHandler;

namespace Orange.HRM.Test
{
    [SetUpFixture]
    class OneTimeAssemblySetUp
    {
        private Report ObjReport => Report.ReportInstance;

        protected void ExecuteSafely(Action steps)
        {
            StepsExecutor.ExecuteSafely(steps);
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            ObjReport.ExtentReportsSetup();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ObjReport.ExtentReportsTearDown();
        }

    }
}
