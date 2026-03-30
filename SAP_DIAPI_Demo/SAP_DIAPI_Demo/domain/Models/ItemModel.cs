using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP_DIAPI_Demo.Models
{
    public class ItemModel
    {

        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int PriceList { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
    }
}
