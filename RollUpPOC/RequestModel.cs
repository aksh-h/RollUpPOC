using Newtonsoft.Json;

namespace RollUpPOC
{
    public partial class RequestModel
    {

        [JsonProperty("eventType")]
        public string EventType { get; set; }

        [JsonProperty("resourceVersion")]
        public string ResourceVersion { get; set; }

        [JsonProperty("resourceContainers")]
        public ResourceContainers ResourceContainers { get; set; }
    }
    public partial class ResourceContainers
    {
        [JsonProperty("collection")]
        public Collection Collection { get; set; }

        [JsonProperty("server")]
        public Collection Server { get; set; }

        [JsonProperty("project")]
        public Collection Project { get; set; }
    }

    public partial class Collection
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }
    }
}
