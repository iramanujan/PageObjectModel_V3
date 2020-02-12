﻿using NUnit.Framework;
using Orange.HRM.Common.Handler.Attributes;
using Orange.HRM.Common.Handler.Browser;
using Orange.HRM.Common.Handler.Log;
using Orange.HRM.TestHarness.Pages.Dashboard;
using Orange.HRM.TestHarness.Pages.Login;
using Orange.HRM.TestHarness.Steps.BaseSetup;
using System;
using WebAutomation.Common.Wait;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace Orange.HRM.TestHarness.Steps.Login
{
    public class LoginStep : OrangeHrmBaseSteps
    {
        private LoginPage ObjLoginPage = null;
        private DashboardPage dashboardPage = null;

        public LoginStep(Browser browser) : base(browser)
        {
            ObjLoginPage = new LoginPage(browser);
            dashboardPage = new DashboardPage(browser);
        }
        public enum ErrorMessageType
        {
            [Description("Username cannot be empty")]
            UserNameEmpty = 0,

            [Description("Password cannot be empty")]
            PasswordEmpty = 1,

            [Description("Invalid credentials")]
            InvalidCredentials = 2
        }

        public void OpenOrangeHrm()
        {
            browser.InitBrowser(appConfigMember.Url);
            browser.WaitTillAjaxLoad(browser.webDriver);
            browser.WaitTillPageLoad(browser.webDriver);
        }

        public void LoginOrangeHRM(string userName, string password)
        {
            ObjReport.Info("Verify Page Url and Text before Login");
            validation.VerifyPageText(ObjLoginPage.pageSource);
            validation.VerifyPageUrl(ObjLoginPage.pageUrl);
            ObjReport.Info("Enter User Name and Password.");
            htmlTextBox.ClearAndSendKeys(ObjLoginPage.UserName, userName);
            htmlTextBox.ClearAndSendKeys(ObjLoginPage.Password, password);
            htmlButton.Submit(ObjLoginPage.Login);
            ObjReport.Info("Verify Page Url and Text after Login");
            validation.VerifyPageText(dashboardPage.pageSource);
            validation.VerifyPageUrl(dashboardPage.pageUrl);
        }
        public void verifyLogin(string userName, string password)
        {
            ObjReport.Info("Verify Page Url and Text before Login");
            validation.VerifyPageText(ObjLoginPage.pageSource);
            validation.VerifyPageUrl(ObjLoginPage.pageUrl);
            ObjReport.Info("Enter User Name and Password.");
            htmlTextBox.ClearAndSendKeys(ObjLoginPage.UserName, userName);
            htmlTextBox.ClearAndSendKeys(ObjLoginPage.Password, password);
            htmlButton.Submit(ObjLoginPage.Login);
            ObjReport.Info("Verify Page Url and Text after Login");
            validation.VerifyPageText(ObjLoginPage.pageSource);
            validation.VerifyPageUrl(ObjLoginPage.pageUrl);

        }
        private void WaitForErrorMessage()
        {
            Waiter.SpinWaitEnsureSatisfied(() =>
            {
                Logger.Info($"Wait For Error Message....");
                try
                {
                    var msg = ObjLoginPage.ErrorMessage;
                    ObjReport.Info("Error Message", msg, true);
                }
                catch (Exception e)
                {
                    ObjReport.Error("Error Message Did Not Show After 5 Sec.", e.Message, true);
                }
                return true;
            }, TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200), $"Could not Set clipboard to text ");
        }
        public void VerifyErrorMessage(ErrorMessageType errorMessageType, string userName, string password)
        {
            if (errorMessageType.Equals(ErrorMessageType.UserNameEmpty))
            {
                ObjReport.Info("Verify User Empty Error Message");
                htmlTextBox.ClearAndSendKeys(ObjLoginPage.Password, password);
                htmlButton.Submit(ObjLoginPage.Login);
                WaitForErrorMessage();
                Assert.AreEqual(ErrorMessageType.UserNameEmpty.GetDescription(), ObjLoginPage.ErrorMessage, "Error Message is not matched.");
            }
            if (errorMessageType.Equals(ErrorMessageType.PasswordEmpty))
            {
                ObjReport.Info("Verify Password Empty Error Message");
                htmlTextBox.ClearAndSendKeys(ObjLoginPage.UserName, userName);
                htmlButton.Submit(ObjLoginPage.Login);
                WaitForErrorMessage();
                Assert.AreEqual(ErrorMessageType.PasswordEmpty.GetDescription(), ObjLoginPage.ErrorMessage, "Error Message is not matched.");
            }
            if (errorMessageType.Equals(ErrorMessageType.InvalidCredentials))
            {
                ObjReport.Info("Verify Invalid Credentials Error Message");
                htmlTextBox.ClearAndSendKeys(ObjLoginPage.UserName, userName);
                htmlTextBox.ClearAndSendKeys(ObjLoginPage.Password, password);
                htmlButton.Submit(ObjLoginPage.Login);
                WaitForErrorMessage();
                Assert.AreEqual(ErrorMessageType.InvalidCredentials.GetDescription(), ObjLoginPage.ErrorMessage, "Error Message is not matched.");
            }
        }

    }
}
