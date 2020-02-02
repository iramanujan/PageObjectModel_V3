using OpenQA.Selenium;

namespace Orange.HRM.Common.Handler.HtmlElement.Element
{
    public class HtmlLink
    {
        public string GetHref(IWebElement webElement)
        {
            return webElement.GetAttribute("href");
        }

        public string GetText(IWebElement webElement)
        {
            return webElement.Text;
        }
    }
}
