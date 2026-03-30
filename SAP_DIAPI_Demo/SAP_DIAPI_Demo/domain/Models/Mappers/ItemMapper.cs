using SAP_DIAPI_Demo.domain.Models.DTOs;
using SAP_DIAPI_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP_DIAPI_Demo.domain.models.Mappers
{
    public static class ItemMapper
    {
        // Chuyển đổi từ Model sang DTO để gửi lên Service Layer
        public static SapItemUpdateRequest ToSLPriceDTO(this ItemModel model)
        {
            if (model == null) return null;

            return new SapItemUpdateRequest
            {
                ItemName = model.ItemName,
                ItemPrices = new List<ItemPriceDTO>
                {
                    new ItemPriceDTO
                    {
                        PriceList = model.PriceList,
                        Price = model.Price,
                        Currency = model.Currency
                    }
                }
            };
        }

        // Sau này bạn có thể thêm các hàm map ngược lại nếu cần
        // public static ItemModel ToDomainModel(this SapItemResponse dto) { ... }
    }
}
