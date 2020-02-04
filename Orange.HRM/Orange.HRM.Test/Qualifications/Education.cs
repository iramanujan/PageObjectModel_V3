using NUnit.Framework;
using Orange.HRM.Common.NUnit.Attributtes;
using Orange.HRM.TestHarness.Pages.Qualifications;
using Orange.HRM.TestHarness.Steps.Login;

namespace Orange.HRM.Test.Qualifications
{
    [TestFixture]
    public class Education : OrangeHrmBaseTest
    {
        private LoginStep ObjLoginStep = null;
        private EducationStep educationStep = null;

        [SetUp]
        public void SetUp()
        {
            this.educationStep = new EducationStep();
            this.ObjLoginStep = new LoginStep();
            this.ObjLoginStep.LoginOrangeHRM(appConfigMember.UserName, appConfigMember.Password);
        }

        [AutoTestCase("MBA", TestName = "Validate Add New Education", Author = "Anuj Jain")]
        [AutoTestCase("B-Tech", TestName = "Validate Add New Education", Author = "Anuj Jain")]
        [AutoTestCase("M-Tech", TestName = "Validate Add New Education", Author = "Anuj Jain")]
        [AutoTestCase("BCA", TestName = "Validate Add New Education", Author = "Anuj Jain")]
        public void ValidateAddEducation(string education)
        {
            this.educationStep.NavigateToEducation();
            this.educationStep.AddEducation(education);
        }

        [TearDown]
        public void TearDown()
        {
            myContext.Browser.CloseAllBrosr();
        }
    }
}
