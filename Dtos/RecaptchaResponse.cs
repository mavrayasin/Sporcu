using System.Text.Json.Serialization;

namespace Sporcu.Dtos
{
    public class RecaptchaResponse
    {
        public bool success { get; set; }
        public float score { get; set; }
        public string action { get; set; }
        [JsonPropertyName("error-codes")]
        public List<string> error_codes { get; set; }
    }

}
