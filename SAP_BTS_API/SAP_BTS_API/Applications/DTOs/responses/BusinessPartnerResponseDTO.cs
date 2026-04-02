using System.Text.Json.Serialization;

namespace SAP_BTS_API.Applications.DTOs.responses
{
    public class BusinessPartnerResponseDTO
    {
        public string CardCode { get; set; } = string.Empty;
        public string CardName { get; set; } = string.Empty;
        public string Phone1 { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FederalTaxID { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public double CurrentBalance { get; set; }
    }
}