﻿using OpenQA.Selenium;
using Orange.HRM.Common.Configuration;
using Orange.HRM.Common.Handler.ActionEvent;
using Orange.HRM.Common.Handler.Browser;
using Orange.HRM.Common.Handler.HtmlElement.Element;
using Orange.HRM.Common.Handler.Log;
using Orange.HRM.TestHarness.Steps.CommonValidation;
using System.Collections.Generic;
using WebAutomation.Common.GenericHelper.ReportHandler;

namespace Orange.HRM.TestHarness.Steps.BaseSetup
{
    public class OrangeHrmBaseSteps
    {
        public Browser browser { get; }
        public IWebDriver webDriver { get; }
        public Validation validation => new Validation(this.browser);
        public readonly AppConfigMember appConfigMember = AppConfigReader.GetToolConfig();
        public Report ObjReport => Report.ReportInstance;
        public OrangeHrmBaseSteps(Browser browser)
        {
            this.browser = browser;
            this.webDriver = browser.webDriver;
        }

        public KeyboardEvents keyboardEvents => KeyboardEvents.KeyboardEventsInstance;

        public MouseEvents mouseEvents => MouseEvents.MouseEventsInstance;

        public HtmlSelect htmlSelect = new HtmlSelect();

        public HtmlRadioButton htmlRadioButton = new HtmlRadioButton();

        public HtmlCheckBox htmlCheckBox = new HtmlCheckBox();

        public HtmlLink htmlLink = new HtmlLink();

        public HtmlTextBox htmlTextBox = new HtmlTextBox();

        public HtmlButton htmlButton = new HtmlButton();



    }
}
