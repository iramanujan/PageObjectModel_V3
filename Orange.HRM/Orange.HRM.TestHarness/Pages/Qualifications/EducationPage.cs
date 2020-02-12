using OpenQA.Selenium;
using Orange.HRM.Common.Handler.Browser;
using Orange.HRM.Common.Handler.HtmlElement;
using Orange.HRM.Common.Handler.Log;
using Orange.HRM.TestHarness.Pages.BaseSetup;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Orange.HRM.TestHarness.Pages.Qualifications
{


    public class EducationPage : OrangeHrmBasePage
    {
        public EducationPage(Browser browser) : base(browser)
        {

        }
        public enum OrderBy
        {
            [Description("Ascending")]
            ASC = 0,

            [Description("Descending ")]
            DESC = 1,
        }

        public enum CellPosition
        {
            [Description("Last")]
            LAST = 0,

            [Description("First")]
            FIRST = 1,

            [Description("VALUE_BASE")]
            VALUE_BASE = 2,
        }

        internal readonly string pageSource = "Leave Entitlements and Usage Report";
        internal readonly string pageUrl = "/index.php/admin/viewEducation";

        internal IWebElement Add => webDriver.FindElement(By.CssSelector("#listActions > #btnAdd.addbutton[value='Add']"));
        internal IWebElement Delete => webDriver.FindElement(By.CssSelector("#btnDel"));
        internal IWebElement Save => webDriver.FindElement(By.CssSelector("#btnSave"));
        internal IWebElement Cancel => webDriver.FindElement(By.CssSelector("#btnCancel"));
        internal IWebElement Level => webDriver.FindElement(By.CssSelector("#education_name"));
        public IWebElement Admin => htmlElement.ElementExists(By.CssSelector("#menu_admin_viewAdminModule"));
        public IWebElement Qualifications => htmlElement.ElementExists(By.CssSelector("#menu_admin_Qualifications"));
        public IWebElement Education => htmlElement.ElementExists(By.CssSelector("#menu_admin_viewEducation"));
        public ReadOnlyCollection<IWebElement> TableHeader => this.webDriver.FindElements(By.CssSelector("#recordsListTable > thead > tr > th"));

        public IWebElement GetEducation(string ColumnName, CellPosition cellPosition = CellPosition.VALUE_BASE, string value = "", string Attribute = TagAttributes.Value, bool IsHyperLink = false)
        {
            IWebElement cell = null;
            int ColumnIndex = htmlElement.GetColumnIndex(TableHeader, ColumnName, ColumnIndex = 1);
            string ColumnCssSelector = "#recordsListTable  > tbody > trCHILD > td:nth-child(" + ColumnIndex + ")";
            if (IsHyperLink)
                ColumnCssSelector = ColumnCssSelector + " > a";

            if (cellPosition.Equals(CellPosition.VALUE_BASE))
            {
                ColumnCssSelector = ColumnCssSelector.Replace("CHILD", "");
                ReadOnlyCollection<IWebElement> cells = this.webDriver.FindElements(By.CssSelector(ColumnCssSelector));
                foreach (var item in cells)
                {
                    try
                    {
                        if (item.GetAttribute(Attribute).Trim().ToLower() == value.Trim().ToLower())
                        {
                            cell = item;
                            break;
                        }
                    }
                    catch
                    {
                        try
                        {
                            if (item.Text.Trim().ToLower() == value.Trim().ToLower())
                            {
                                cell = item;
                                break;
                            }
                        }
                        catch (Exception ObjException2)
                        {
                            Logger.Error("Error Occured At Column" + ObjException2.Message);
                        }
                    }
                }
            }
            if (cellPosition.Equals(CellPosition.LAST))
            {
                ColumnCssSelector = ColumnCssSelector.Replace("CHILD", ":last-child");
                cell = this.webDriver.FindElement(By.CssSelector(ColumnCssSelector));
            }
            if (cellPosition.Equals(CellPosition.FIRST))
            {
                ColumnCssSelector = ColumnCssSelector.Replace("CHILD", ":first-child");
                cell = this.webDriver.FindElement(By.CssSelector(ColumnCssSelector));
            }
            return cell;
        }
    }
}
