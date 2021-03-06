using System.Text.Json.Serialization;

namespace Tides.Models.Auth
{
    public class AuthKeyOptions
    {
        [JsonPropertyName("type")]
        public AuthType Type { get; set; }
        
        [JsonPropertyName("format")]
        public AuthKeyFormat Format { get; set; }
    }
}