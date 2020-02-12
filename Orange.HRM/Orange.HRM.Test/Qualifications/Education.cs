using NUnit.Framework;
using Orange.HRM.Common.NUnit.Attributtes;

namespace Orange.HRM.Test.Qualifications
{
    [TestFixture]
    public class Education : OrangeHrmBaseTest
    {
        [SetUp]
        public void SetUp()
        {
            ObjReport.CreateTest(TestContext.CurrentContext.Test.Name);
            loginStep.OpenOrangeHrm();
            loginStep.LoginOrangeHRM(appConfigMember.UserName, appConfigMember.Password);
        }

        [AutoTestCase("MBA", TestName = "Validate Add New Education", Author = "Anuj Jain")]
        //[AutoTestCase("B-Tech", TestName = "Validate Add New Education", Author = "Anuj Jain")]
        //[AutoTestCase("M-Tech", TestName = "Validate Add New Education", Author = "Anuj Jain")]
        //[AutoTestCase("BCA", TestName = "Validate Add New Education", Author = "Anuj Jain")]
        public void ValidateAddEducation(string education)
        {
            educationStep.NavigateToEducation();
            educationStep.AddEducation(education);
        }

        [TearDown]
        public void TearDown()
        {
            ObjReport.ExtentReportsTearDown();

        }


    }
}
