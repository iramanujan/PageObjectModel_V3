using OpenQA.Selenium;

namespace Orange.HRM.Common.Handler.HtmlElement.Element
{
    public class HtmlButton
    {
        public void Submit(IWebElement webElement)
        {
            webElement.Submit();
        }

        public string Text(IWebElement webElement)
        {
            return webElement.Text;
        }
    }
}
