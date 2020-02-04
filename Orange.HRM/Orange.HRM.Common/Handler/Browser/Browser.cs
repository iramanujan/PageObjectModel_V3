using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Orange.HRM.Common.Configuration;
using Orange.HRM.Common.Handler.Driver.Intrface;
using Orange.HRM.Common.Handler.Log;
using Orange.HRM.Common.Helper;
using Orange.HRM.Common.Wait;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using WebAutomation.Common.GenericHelper.ReportHandler;
using WebAutomation.Common.Wait;
using static Orange.HRM.Common.Configuration.AppConfigMember;

namespace Orange.HRM.Common.Handler.Browser
{
    public class Browser : JavaScript, ITakesScreenshot, IWebDriver
    {
        protected new static readonly AppConfigMember appConfigMember = AppConfigReader.GetToolConfig();
        private WaitExpectedConditions waitExpectedConditions = null;
        public Report ObjReport => Report.ReportInstance;
        private readonly IWebDriverFactory webDriverFactory;
        public IWebDriver objWebDriver;
        public IWebDriver webDriver => objWebDriver ?? (objWebDriver = webDriverFactory.InitializeWebDriver());

        #region Constructor
        public Browser(IWebDriverFactory webDriverFactory)
        {
            BackupReportAndLog();
            CleanupCreatedDirectoriesSafely();
            CreateUploadDwonloadDirectory();
            this.webDriverFactory = webDriverFactory;
            ObjReport.ExtentReportsSetup();
            waitExpectedConditions = new WaitExpectedConditions(webDriver);
        }

        public Browser(IWebDriver webDriver)
        {
            this.objWebDriver = webDriver;
        }

        public Browser()
        {
        }
        #endregion

        #region Functions

        public IWebDriver GetWebDriverObject()
        {
            return this.webDriver;
        }

        public Screenshot GetScreenshot()
        {
            return ((ITakesScreenshot)webDriver).GetScreenshot();
        }

        public Browser InitBrowser(string url)
        {
            webDriver.Navigate().GoToUrl(url);
            WaitTillAjaxLoad(webDriver);
            WaitTillPageLoad(webDriver);
            Maximize();
            webDriver.SwitchTo().DefaultContent();
            return this;
        }

        public Browser OpenUrl(string url)
        {
            this.webDriver.Navigate().GoToUrl(url);
            return this;
        }

        public Browser Maximize()
        {
            this.webDriver.Manage().Window.Maximize();
            return this;
        }

        public Browser Forward()
        {
            this.webDriver.Navigate().Forward();
            return this;
        }

        public Browser ClearCookies()
        {
            this.webDriver.Manage().Cookies.DeleteAllCookies();
            return this;
        }

        public Browser Back()
        {
            this.webDriver.Navigate().Back();
            return this;
        }

        public Browser Refresh()
        {
            webDriver.Navigate().Refresh();
            WaitTillPageLoad(webDriver);
            return this;
        }

        public void CloseCurrentTab()
        {
            this.webDriver.Close();
        }

        public string GetUrl()
        {
            return this.webDriver.Url;
        }
        public string GetDownloadPath()
        {
            return appConfigMember.RootDownloadLocation;
        }

        public string GetUploadPath()
        {
            return appConfigMember.RootUploadLocation;
        }

        public string GetDecodedUrl()
        {
            var url = webDriver.Url;
            return HttpUtility.UrlDecode(url);
        }

        public void Close()
        {
            this.webDriver.Close();
        }

        public void Dispose()
        {
            Quit();
        }

        public void Quit()
        {
            if (this.webDriver != null)
            {
                try
                {
                    foreach (var window in this.webDriver.WindowHandles)
                    {
                        SwitchToWindowHandle(window);
                        Close();
                    }
                    this.webDriver.Quit();
                }

                catch (Exception ex)
                {
                    Logger.Error($"Unable to Quit the browser. Reason: {ex.Message}");
                    switch (appConfigMember.Browser)
                    {
                        case BrowserType.IE:

                            ProcessUtils.KillProcesses("iexplore");
                            ProcessUtils.KillProcesses("IEDriverServer");
                            break;
                        case BrowserType.Chrome:
                            ProcessUtils.KillProcesses("chrome");
                            ProcessUtils.KillProcesses("chromedriver");
                            ProcessUtils.KillProcesses("chrome.exe");
                            ProcessUtils.KillProcesses("chromedriver.exe");
                            break;
                        case BrowserType.Firefox:
                            ProcessUtils.KillProcesses("firefox.exe");
                            ProcessUtils.KillProcesses("geckodriver.exe");
                            ProcessUtils.KillProcesses("firefox");
                            ProcessUtils.KillProcesses("geckodriver");
                            break;
                    }
                }

                finally
                {
                    this.objWebDriver = null;
                }
            }
        }

        public IOptions Manage()
        {
            return this.webDriver.Manage();
        }

        public INavigation Navigate()
        {
            return this.webDriver.Navigate();
        }

        public ITargetLocator SwitchTo()
        {
            return this.webDriver.SwitchTo();
        }

        public IWebElement FindElement(By by)
        {
            return this.webDriver.FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return this.webDriver.FindElements(by);
        }

        public IWebElement GetElementIfExists(By by)
        {
            return this.webDriver.FindElements(by).FirstOrDefault();
        }

        public void CleanupCreatedDirectoriesSafely()
        {
            if (Directory.Exists(appConfigMember.AutomationLogPath))
            {
                StepsExecutor.ExecuteSafely(() => Directory.Delete(appConfigMember.AutomationLogPath, true));
            }
            if (Directory.Exists(appConfigMember.AutomationReportPath))
            {
                StepsExecutor.ExecuteSafely(() => Directory.Delete(appConfigMember.AutomationReportPath, true));
            }
            if (Directory.Exists(GetDownloadPath()))
            {
                StepsExecutor.ExecuteSafely(() => Directory.Delete(GetDownloadPath(), true));
            }
            if (Directory.Exists(GetUploadPath()))
            {
                StepsExecutor.ExecuteSafely(() => Directory.Delete(GetUploadPath(), true));
            }
        }

        public void CreateUploadDwonloadDirectory()
        {
            if (!Directory.Exists(appConfigMember.AutomationLogPath))
            {
                StepsExecutor.ExecuteSafely(() => Directory.CreateDirectory(appConfigMember.AutomationLogPath));
            }
            if (!Directory.Exists(appConfigMember.AutomationReportPath))
            {
                StepsExecutor.ExecuteSafely(() => Directory.CreateDirectory(appConfigMember.AutomationReportPath));
            }
            if (!Directory.Exists(GetDownloadPath()))
            {
                StepsExecutor.ExecuteSafely(() => Directory.CreateDirectory(GetDownloadPath()));
            }
            if (!Directory.Exists(GetUploadPath()))
            {
                StepsExecutor.ExecuteSafely(() => Directory.CreateDirectory(GetUploadPath()));
            }
        }

        public void BackupReportAndLog()
        {
            var newLogPath = appConfigMember.AutomationLogPath.Replace(appConfigMember.AutomationLogFolderName, appConfigMember.AutomationLogFolderName + TimeUtil.GetTimeStamp());
            var newReportPath = appConfigMember.AutomationReportPath.Replace(appConfigMember.AutomationReportFolderName, appConfigMember.AutomationReportFolderName + TimeUtil.GetTimeStamp());
            if (Directory.Exists(appConfigMember.AutomationLogPath))
            {
                StepsExecutor.ExecuteSafely(() => Directory.Move(appConfigMember.AutomationLogPath, newLogPath));
            }
            if (Directory.Exists(appConfigMember.AutomationReportPath))
            {
                StepsExecutor.ExecuteSafely(() => Directory.Move(appConfigMember.AutomationReportPath, newReportPath));
            }
        }

        public void CloseAllBrosr()
        {
            CleanupCreatedDirectoriesSafely();
            Quit();
            switch (appConfigMember.Browser)
            {
                case BrowserType.IE:
                    //Killing IE driver process if exists
                    ProcessUtils.KillProcesses("iexplore");
                    ProcessUtils.KillProcesses("IEDriverServer");
                    break;
                case BrowserType.Chrome:
                    ProcessUtils.KillProcesses("chrome");
                    ProcessUtils.KillProcesses("chromedriver");
                    ProcessUtils.KillProcesses("chrome.exe");
                    ProcessUtils.KillProcesses("chromedriver.exe");
                    break;
                case BrowserType.Firefox:
                    ProcessUtils.KillProcesses("firefox.exe");
                    ProcessUtils.KillProcesses("geckodriver.exe");
                    ProcessUtils.KillProcesses("firefox");
                    ProcessUtils.KillProcesses("geckodriver");
                    break;
            }
        }

        public string GetTitle()
        {
            return this.webDriver.Title;
        }

        public void CloseTab()
        {
            this.webDriver.Close();
        }

        public void getJavaScriptConsoleLogs()
        {
            Logger.Info("====================================================");
            Logger.Info("Browser Console logs Starts:-");
            IReadOnlyCollection<LogEntry> logEntries = Manage().Logs.GetLog(LogType.Browser);
            foreach (var logEntry in logEntries)
            {
                Logger.Info(logEntry.Timestamp + " - " + logEntry.Message);
            }
            Logger.Info("Browser Console logs Ends:-");
            Logger.Info("====================================================");
        }
        #endregion

        #region WindowHandle
        public ReadOnlyCollection<string> GetWindowHandles()
        {
            return this.webDriver.WindowHandles;
        }
        public void SwitchToWindowHandle(string windowName)
        {
            waitExpectedConditions.WaitTillSwitchToWindowHandle(windowName);
        }
        public void SwitchHandleToNewWindowByPartialUrl(string urlPart)
        {

            if (GetDecodedUrl().Contains(urlPart))
            {
                return;
            }
            ReadOnlyCollection<string> handles = webDriver.WindowHandles;
            foreach (string handle in handles.Reverse())
            {
                waitExpectedConditions.WaitTillSwitchToWindowHandle(handle);
                if (GetDecodedUrl().Contains(urlPart))
                {
                    WaitTillPageLoad(webDriver);
                    WaitTillAjaxLoad(webDriver);
                    return;
                }
            }
        }
        #endregion

        #region Frame
        public IWebDriver SwitchToFrameByIndex(int frameIndex)
        {
            return waitExpectedConditions.WaitTillSwitchToFrameByIndex(frameIndex);
        }
        public IWebDriver SwitchToFrameByFrameName(string frameName)
        {
            return waitExpectedConditions.WaitTillSwitchToFrameByFrameName(frameName);
        }
        public IWebDriver SwitchToFrameByWebElement(IWebElement webElement)
        {
            return waitExpectedConditions.WaitTillSwitchToFrameByWebElement(webElement);
        }
        #endregion

        #region Alert
        public IAlert SwitchToAlert()
        {
            return waitExpectedConditions.WaitTillSwitchToAlert();
        }
        public void AcceptAlert(IAlert alert)
        {
            waitExpectedConditions.WaitTillAcceptAlert(alert);
            SwitchTo().DefaultContent();
        }
        public void AcceptAlert(IAlert alert, string value)
        {
            waitExpectedConditions.WaitTillAcceptAlert(alert, value);
            SwitchTo().DefaultContent();
        }
        public void DismissAlert(IAlert alert)
        {
            waitExpectedConditions.WaitTillDismissAlert(alert);
            SwitchTo().DefaultContent();
        }
        public void DismissAlert(IAlert alert, string value)
        {
            waitExpectedConditions.WaitTillDismissAlert(alert, value);
            SwitchTo().DefaultContent();
        }
        public void SendKeysAlert(IAlert alert, string value)
        {
            alert.SendKeys(value);
            SwitchTo().DefaultContent();
        }
        public String GetAlertText(IAlert alert)
        {
            return alert.Text;
        }
        #endregion

        #region Properties
        public string Url
        {
            get { return this.webDriver.Url; }
            set { this.webDriver.Url = value; }
        }

        public string Title
        {
            get { return this.webDriver.Title; }
        }

        public string PageSource
        {
            get { return this.webDriver.PageSource; }
        }

        public string CurrentWindowHandle
        {
            get { return this.webDriver.CurrentWindowHandle; }
        }

        public ReadOnlyCollection<string> WindowHandles
        {
            get { return this.webDriver.WindowHandles; }
        }
        #endregion

    }

}