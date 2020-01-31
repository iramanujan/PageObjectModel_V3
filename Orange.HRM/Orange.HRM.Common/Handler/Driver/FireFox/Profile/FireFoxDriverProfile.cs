using Orange.HRM.Common.Configuration;
using OpenQA.Selenium.Firefox;
using System;

namespace WebDriverHelper.DriverFactory.FireFox.Profile
{
    internal class FireFoxDriverProfile
    {
        private static readonly AppConfigMember appConfigMember = AppConfigReader.GetToolConfig();
        internal static FirefoxProfile CreateProfile()
        {
            FirefoxProfile profile;
            try
            {
                profile = appConfigMember.ProfileName == null ? new FirefoxProfile() : new FirefoxProfile(appConfigMember.ProfileName);
                profile.SetPreference("browser.download.folderList", 2);
                profile.SetPreference("browser.download.dir", appConfigMember.RootDownloadLocation);
                profile.SetPreference("browser.download.manager.alertOnEXEOpen", false);
                profile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "text/plain, application/octet-stream");
                if (appConfigMember.NoCache)
                {
                    profile.SetPreference("browser.cache.disk.enable", false);
                    profile.SetPreference("browser.cache.memory.enable", false);
                    profile.SetPreference("browser.cache.offline.enable", false);
                }
                profile.SetPreference("intl.accept_languages", appConfigMember.Localization.ToString());
            }
            catch (Exception)
            {
                profile = new FirefoxProfile();
            }
            return profile;
        }

        internal static FirefoxOptions Options()
        {
            FirefoxOptions firefoxOptions = new FirefoxOptions();
            firefoxOptions.SetPreference(FirefoxDriver.ProfileCapabilityName, FireFoxDriverProfile.CreateProfile().ToBase64String());
            firefoxOptions.ToCapabilities();

            return null;
        }
    }
}
