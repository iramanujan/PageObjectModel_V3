using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Orange.HRM.Common.Handler.Driver.Base;
using Orange.HRM.Common.Handler.Driver.Grid;
using Orange.HRM.Common.Handler.Driver.Intrface;
using Orange.HRM.Common.Handler.Log;
using Orange.HRM.Common.Location.Download;
using Orange.HRM.Common.Location.Upload;
using System;
using WebDriverHelper.DriverFactory.Chrome.Options;

namespace WebDriverHelper.DriverFactory.Chrome.Remote
{
    public class RemoteChromeDriver : BaseRemoteDriverFactory, IWebDriverFactory
    {
        private IWebDriver webDriver = null;

        protected override ICapabilities Capabilities => ChromeDriverOptions.CreateDefaultChromeOptions().ToCapabilities();

        private void BeforeWebDriverSetupSetps()
        {
            downloadLocation = new Lazy<string>(() => DownloadLocation.CreateWebDriverDirectory(appConfigMember.Browser.ToString() + appConfigMember.ExecutionType.ToString(), appConfigMember.RootDownloadLocation));
            uploadLocation = new Lazy<UploadLocation>(() => UploadLocation.Create(appConfigMember.Browser.ToString() + appConfigMember.ExecutionType.ToString(), true, appConfigMember.RootUploadLocation));
        }

        public IWebDriver InitializeWebDriver()
        {
            BeforeWebDriverSetupSetps();
            Logger.Info($"ATTEMPT TO CREATE REMOTE {browserName.ToUpper()} DRIVER");
            var remoteWebDriver = new RemoteWebDriver(new Uri(gridUrl), Capabilities, TimeSpan.FromMilliseconds(commandTimeout));
            Logger.Info($"CREATED REMOTE {browserName.ToUpper()} DRIVER ON HOST {GridConfigHelper.GetRemoteDriverHostName(remoteWebDriver, gridHost)}");
            webDriver = remoteWebDriver;
            return webDriver;
        }
    }
}
