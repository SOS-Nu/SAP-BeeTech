using System.Text.Json.Serialization;

namespace SAP_BTS_API.Infrastructure.Persistence.SAP_DTOs.SAPrequests
{
    public class BusinessPartnerRequestSAP
    {
        private string? _cardCode;
        public string? CardCode
        {
            get => _cardCode;
            set => _cardCode = string.IsNullOrWhiteSpace(value) ? null : value;
        }

        private string? _cardName;
        // Quy tắc của Vua ep buộc gửi null lên SAP để cập nhật DB về null
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public string? CardName
        {
            get => _cardName;
            set => _cardName = string.IsNullOrWhiteSpace(value) ? null : value;
        }

        private string? _phone1;
        public string? Phone1
        {
            get => _phone1;
            set => _phone1 = string.IsNullOrWhiteSpace(value) ? null : value;
        }

        public string? Email { get; set; }

        public string? FederalTaxID { get; set; }
    }
}