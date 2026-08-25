using System.Text.Json.Serialization;

namespace LabCiberSeguridad.Models
{
    public class ResendEmailRequest
    {
        [JsonPropertyName("from")]
        public string From { get; set; }

        [JsonPropertyName("to")]
        public string To { get; set; }

        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("html")]
        public string Html { get; set; }
    }
}