using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Orange.HRM.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Orange.HRM.Common.Handler.HtmlElement.Element
{
    public class HtmlSelect
    {
        public SelectElement GetSelect(IWebElement webElement)
        {
            return new SelectElement(webElement);
        }

        public void SelectByIndex(IWebElement webElement, int index)
        {
            var selectElement = GetSelect(webElement);
            selectElement.SelectByIndex(index);
        }

        public void SelectByValue(IWebElement webElement, string value)
        {
            var selectElement = GetSelect(webElement);
            selectElement.SelectByValue(value);
        }

        public void SelectByText(IWebElement webElement, string text)
        {
            var selectElement = GetSelect(webElement);
            selectElement.SelectByText(text);
        }

        public string SelectedOption(IWebElement webElement)
        {
            var selectElement = GetSelect(webElement);
            return selectElement.SelectedOption.Text;
        }

        public List<String> GetOptions(IWebElement webElement)
        {
            List<String> Options = new List<string>();
            var selectElement = GetSelect(webElement);
            Options = selectElement.Options.Select(item => item.Text.ToEnum<String>()).ToList();
            return Options;
        }

        public void DeselectAll(IWebElement webElement)
        {
            var selectElement = GetSelect(webElement);
            selectElement.DeselectAll();
        }

        public void DeselectByIndex(IWebElement webElement, int index)
        {
            var selectElement = GetSelect(webElement);
            selectElement.DeselectByIndex(index);
        }

        public void DeselectByValue(IWebElement webElement, string value)
        {
            var selectElement = GetSelect(webElement);
            selectElement.DeselectByValue(value);
        }

        public void DeselectByText(IWebElement webElement, string text)
        {
            var selectElement = GetSelect(webElement);
            selectElement.DeselectByText(text);
        }

        public Boolean IsMultiple(IWebElement webElement)
        {
            var selectElement = GetSelect(webElement);
            return selectElement.IsMultiple;
        }
    }
}