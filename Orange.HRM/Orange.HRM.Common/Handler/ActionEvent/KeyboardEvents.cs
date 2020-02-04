using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Orange.HRM.Common.Handler.Browser;

namespace Orange.HRM.Common.Handler.ActionEvent
{
    public class KeyboardEvents
    {

        private readonly IWebDriver ObjWebDriver;
        private readonly Actions ObjActions;
        public static KeyboardEvents KeyboardEventsInstance { get; } = new KeyboardEvents();
        private KeyboardEvents(IWebDriver webDriver)
        {
            this.ObjWebDriver = webDriver;
            this.ObjActions = new Actions(this.ObjWebDriver);
        }
        private KeyboardEvents() : this(BrowserContext.browser.webDriver)
        {

        }

        static KeyboardEvents()
        {
        }
       
        public void KeyDown(IWebElement element, string Key)
        {
            this.ObjActions.KeyDown(element, Key).Build().Perform();
        }

        public void KeyUp(IWebElement element, string Key)
        {
            this.ObjActions.KeyUp(element, Key).Build().Perform();
        }

        public void SendKeys(IWebElement element, string keysToSend)
        {
            this.ObjActions.SendKeys(element, keysToSend).Build().Perform();
        }
    }
}
