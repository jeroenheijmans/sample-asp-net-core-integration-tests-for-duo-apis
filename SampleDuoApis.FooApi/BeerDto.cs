using System.Text.Json.Serialization;

namespace SampleDuoApis.FooApi
{
    public class BeerDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}