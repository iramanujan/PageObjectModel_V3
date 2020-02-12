using Orange.HRM.Common.Handler.Browser;
using Orange.HRM.TestHarness.Steps.BaseSetup;
using Orange.HRM.TestHarness.Steps.CommonValidation;
using System;
using WebAutomation.Common.Wait;
using static Orange.HRM.TestHarness.Pages.Qualifications.EducationPage;

namespace Orange.HRM.TestHarness.Pages.Qualifications
{
    public class EducationStep : OrangeHrmBaseSteps
    {
        EducationPage educationPage = null;
        public EducationStep(Browser browser) :base(browser)
        {
            this.educationPage = new EducationPage(browser);
        }

        public void NavigateToEducation()
        {
            browser.WaitTillPageLoad(webDriver);
            browser.WaitTillAjaxLoad(webDriver);
            educationPage.Admin.Click();
            educationPage.Qualifications.Click();
            educationPage.Education.Click();
            browser.WaitTillPageLoad(webDriver);
            browser.WaitTillAjaxLoad(webDriver);
            validation.VerifyPageText(educationPage.pageSource);
            validation.VerifyPageUrl(educationPage.pageUrl);
        }

        public void AddEducation(string education)
        {
            educationPage.Add.Click();
            htmlTextBox.ClearAndSendKeys(educationPage.Level, education);
            htmlButton.Submit(educationPage.Save);
            var actualEducation = educationPage.GetEducation("Level", CellPosition.VALUE_BASE, education, IsHyperLink: true).Text;
            validation.VerifyText(education, actualEducation, "Validate Education Level Added Successfully.");
        }

        public void DeleteEducation(string education)
        {
            htmlButton.Submit(educationPage.Delete);
        }
    }
}
