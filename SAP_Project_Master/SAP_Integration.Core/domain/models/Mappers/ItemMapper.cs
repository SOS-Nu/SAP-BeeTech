using SAP_Integration.Core.domain.Models.DTOs;
using SAP_Integration.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP_Integration.Core.domain.models.Mappers
{
    public static class ItemMapper
    {
        // Chuyển đổi từ Model sang DTO để gửi lên Service Layer
        public static ItemUpdateRequest ToSLPriceDTO(this ItemModel model)
        {
            if (model == null) return null;

            return new ItemUpdateRequest
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

        // public static ItemModel ToDomainModel(this SapItemResponse dto) { ... }
    }
}
