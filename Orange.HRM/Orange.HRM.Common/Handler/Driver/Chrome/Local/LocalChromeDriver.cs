using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Orange.HRM.Common.Handler.Driver.Base;
using Orange.HRM.Common.Handler.Driver.Intrface;
using Orange.HRM.Common.Helper;
using System;
using WebDriverHelper.DriverFactory.Chrome.Options;

namespace WebDriverHelper.DriverFactory.Chrome.Local
{
    public class LocalChromeDriver : BaseLocalDriverFactory, IWebDriverFactory
    {
        private IWebDriver webDriver = null;
        private ChromeOptions chromeOptions = null;
        private ChromeDriverService chromeDriverService = null;

        private void BeforeWebDriverSetupSetps()
        {
            this.chromeOptions = ChromeDriverOptions.CreateDefaultChromeOptions();
            this.chromeDriverService = ChromeDriverService.CreateDefaultService(FileUtils.GetCurrentlyExecutingDirectory());
        }

        public IWebDriver InitializeWebDriver()
        {
            BeforeWebDriverSetupSetps();
            webDriver = new ChromeDriver(chromeDriverService, chromeOptions, TimeSpan.FromSeconds(appConfigMember.CommandTimeout));
            return this.webDriver;
        }
    }
}
