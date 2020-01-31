using Newtonsoft.Json;

namespace Orange.HRM.Common.Handler.Driver.Grid
{
    public class SlotCountModel
    {
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }

        [JsonProperty(PropertyName = "free")]
        public int Free { get; set; }
    }
}
