using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.Remote;
using Orange.HRM.Common.Handler.Log;
using Orange.HRM.Common.Helper;
using System;
using System.IO;
using System.Linq;
using System.Net;
using WebAutomation.Common.Wait;


namespace Orange.HRM.Common.Handler.Driver.Grid
{
    public static class GridConfigHelper
    {
        public static string GetRemoteDriverHostName(RemoteWebDriver remoteWebDriver, string gridHostUrl)
        {
            string url = $"{gridHostUrl}grid/api/testsession?session={remoteWebDriver.SessionId}";
            string nodeHost = null;
            using (var client = new WebClient())
            {
                using (var stream = client.OpenRead(url))
                {
                    if (stream != null)
                        using (var reader = new StreamReader(stream))
                        {
                            var jObject = JObject.Parse(reader.ReadLine());
                            nodeHost = new Uri(jObject.GetValue("proxyId").ToString()).Host;
                        }
                }
            }
            return nodeHost;
        }


        public static void WaitForFreeSlotOnHubForBrowser(string gridHostUrl, TimeSpan waitTimeout, string capabilityBrowserName)
        {
            string url = $"{gridHostUrl}grid/admin/ExtendedHubStatusServlet";

            var waitForFreeSlotsFunc = new Func<bool>(() =>
            {
                using (var client = new WebClient())
                {
                    client.QueryString.Add("configuration", "browserSlotsCount");
                    var resultString = client.DownloadString(url);
                    var browserStatus = JsonConvert.DeserializeObject<BrowsersStatusModel>(resultString);
                    var slotCount = browserStatus.BrowserSlotsCount.GetPropertyValuesWithFilteredAttribute<SlotCountModel, JsonPropertyAttribute>(
                            x => string.Equals(capabilityBrowserName.ToUpper(), x.PropertyName)).FirstOrDefault();

                    Logger.Info($"Number of free slots for browser '{capabilityBrowserName}' is {(slotCount == null ? "null" : Convert.ToString(slotCount.Free))}");
                    return slotCount != null && slotCount.Free > 0;
                }
            });
            Logger.Info("Wait for free slot on the grid");
            Waiter.SpinWait(waitForFreeSlotsFunc, waitTimeout, TimeSpan.FromMilliseconds(1000));
        }
    }
}
