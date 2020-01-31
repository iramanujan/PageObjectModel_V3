using Newtonsoft.Json;

namespace Orange.HRM.Common.Handler.Driver.Grid
{
    public class BrowsersStatusModel
    {
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        [JsonProperty(PropertyName = "browserSlotsCount")]
        public BrowserSlotsCountModel BrowserSlotsCount { get; set; }
    }
}
