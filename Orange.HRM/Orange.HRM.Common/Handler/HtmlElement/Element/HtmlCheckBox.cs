using OpenQA.Selenium;

namespace Orange.HRM.Common.Handler.HtmlElement.Element
{
    public class HtmlCheckBox
    {

        public string Text(IWebElement webElement)
        {
            return webElement.Text;
        }

        private void Click()
        {
            Click();
        }

        public void Select(IWebElement webElement)
        {
            if (!webElement.Selected)
            {
                Click();
            }
        }


        public void Deselect(IWebElement webElement)
        {
            if (webElement.Selected)
            {
                Click();
            }
        }

        public void Set(bool value, IWebElement webElement)
        {
            if (value)
            {
                Select(webElement);
            }
            else
            {
                Deselect(webElement);
            }
        }
    }
}
