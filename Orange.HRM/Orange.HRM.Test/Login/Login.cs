using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Orange.HRM.Common.NUnit.Attributtes;
using Orange.HRM.Test.BaseSetup;
using Orange.HRM.TestHarness.Steps.Login;

namespace Orange.HRM.Test.Login
{
    [TestFixture]
    public class Login : OrangeHrmBaseTest
    {
        private LoginStep ObjLoginStep;

        [SetUp]
        public void SetUp()
        {
            this.ObjLoginStep = new LoginStep();
        }

   
        [AutoTestCase("Admin", "admin123", TestName = "Validate Login With Valid User.", Author = "Anuj Jain")]
        public void ValidateLogin(string username, string password)
        {
            this.ObjLoginStep.LoginOrangeHRM(username, password);
        }
       
        [AutoTestCase("Admin", "admin1234", TestName = "Validate Login With InValid User.", Author = "Anuj Jain")]
        public void ValidateInValidLogin(string username, string password)
        {
            this.ObjLoginStep.verifyLogin(username, password);
        }
     
        [TestCase(LoginStep.ErrorMessageType.UserNameEmpty, "", "admin123", TestName = "Validate Error Message Username cannot be empty.", Author = "Anuj Jain")]
        [TestCase(LoginStep.ErrorMessageType.PasswordEmpty, "Admin", "", TestName = "Validate Error Message Password cannot be empty.", Author = "Anuj Jain")]
        [TestCase(LoginStep.ErrorMessageType.PasswordEmpty, "admin123", "Admin", TestName = "Validate Error Message Invalid credentials.", Author = "Anuj Jain")]
        public void ValidateErrorMessage(LoginStep.ErrorMessageType errorMessageType, string userName, string password)
        {
            this.ObjLoginStep.VerifyErrorMessage(errorMessageType, userName, password);
        }

        [TearDown]
        public void TearDown()
        {
          
        }
    }
}
