using Orange.HRM.Common.Configuration;
using OpenQA.Selenium.Chrome;

namespace WebDriverHelper.DriverFactory.Chrome.Options
{
    class ChromeDriverOptions
    {
        private static readonly AppConfigMember appConfigMember = AppConfigReader.GetToolConfig();
        public static ChromeOptions CreateDefaultChromeOptions()
        {
            var options = new ChromeOptions();

            options.AddUserProfilePreference("safebrowsing.enabled", true);
            options.AddUserProfilePreference("download.default_directory", appConfigMember.RootDownloadLocation);

            options.AddArguments("--test-type");
            options.AddArguments("--no-sandbox");
            options.AddArgument("--start-maximized");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--incognito");
            options.AddArgument("--enable-precise-memory-info");
            options.AddArgument("--disable-default-apps");
            options.AddArgument("test-type=browser");
            options.AddArgument("disable-infobars");

            if (appConfigMember.NoCache)
            {
                options.AddArguments("--incognito");
            }

            options.AddArguments($"--lang={appConfigMember.Localization.ToString()}");

            return options;
        }
    }
}
