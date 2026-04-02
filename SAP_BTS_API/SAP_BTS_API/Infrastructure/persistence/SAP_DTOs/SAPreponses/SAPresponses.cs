using System.Text.Json.Serialization;

namespace SAP_BTS_API.Infrastructure.persistence.SAP_DTOs.SAPreponses
{
    public class SapErrorResponse
    {
        public SapErrorDetail Error { get; set; }
    }

    public class SapErrorDetail
    {
        public int Code { get; set; }
        public SapErrorMessage Message { get; set; }
    }

    public class SapErrorMessage
    {
        public string Lang { get; set; }
        public string Value { get; set; }
    }

    public class SapCollectionResponse<T>
    {
        [JsonPropertyName("value")]
        public List<T> Value { get; set; } = new();
    }
}