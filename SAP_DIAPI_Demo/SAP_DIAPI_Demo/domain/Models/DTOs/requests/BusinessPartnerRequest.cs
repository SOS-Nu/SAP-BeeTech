using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP_DIAPI_Demo.domain.Models.DTOs
{
    public class BusinessPartnerRequest
    {

        //khi quy định xong thì ở đây là luật cao nhất
        //,giống như vua nên ở repo có ingore thì vẫn còn nếu là include ở đây

        //không gửi luôn
        private string _cardCode;
        [JsonProperty("CardCode", NullValueHandling = NullValueHandling.Ignore)]
        public string CardCode
        {
            get => _cardCode;
            set => _cardCode = string.IsNullOrWhiteSpace(value) ? null : value;
        }


        //Nhấn Enter -> Biến thành null -> VẪN GỬI chữ "null" lên SAP
        //nếu SAP cho null thì data trong db về null
        private string _cardName;
        [JsonProperty("CardName", NullValueHandling = NullValueHandling.Include)]
        public string CardName
        {
            get => _cardName;
            set => _cardName = string.IsNullOrWhiteSpace(value) ? null : value;
        }


        private string _phone1;

        [JsonProperty("Phone1", NullValueHandling = NullValueHandling.Ignore)]
        public string Phone1
        {
            get => _phone1;
            set => _phone1 = string.IsNullOrWhiteSpace(value) ? null : value;
        }


        [JsonProperty("Email")]
        public string Email { get; set; }


        // //vẫn gửi
        [JsonProperty("FederalTaxID")]
        public string FederalTaxID { get; set; }

    }

    
}