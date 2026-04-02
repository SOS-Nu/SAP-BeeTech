

namespace SAP_BTS_API.domain.models.mappers
{
    public class BusinessPartnerModel
    {
        public string CardCode { get; set; } = string.Empty;
        public string? CardName { get; set; }
        public string? Phone1 { get; set; }
        public string? Email { get; set; }
        public string? FederalTaxID { get; set; }
        public string? Address { get; set; }
        public double? Balance { get; set; }
    }
}