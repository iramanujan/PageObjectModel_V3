using OpenQA.Selenium;


namespace Orange.HRM.Common.Handler.HtmlElement.Element
{
    public class HtmlRadioButton
    {

        public string Text(IWebElement webElement)
        {
            return webElement.Text;
        }
        
        public void Select(IWebElement webElement)
        {
            if (!webElement.Selected)
            {
                webElement.Click();
            }
        }

        public void DeSelect(IWebElement webElement)
        {
            if (webElement.Selected)
            {
                Click(webElement);
            }
        }

        public void Set(IWebElement webElement, bool value)
        {
            if (value)
            {
                Select(webElement);
            }
            else
            {
                DeSelect(webElement);
            }
        }

        public void Click(IWebElement webElement)
        {
            webElement.Click();
        }

    }
}
