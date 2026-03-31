using SAP_DIAPI_Demo.domain.Models.DTOs;
using SAP_DIAPI_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq; 

namespace SAP_DIAPI_Demo.domain.models.Mappers
{
    public static class SalesOrderMapper
    {
        public static SalesOrderRequest ToSLSalesOrderDTO(this SalesOrderModel model)
        {
            if (model == null) return null;

            return new SalesOrderRequest
            {
                CardCode = model.CardCode,
                DocDueDate = model.DocDueDate.ToString("yyyy-MM-dd"),
                Comments = model.Comments,
                BPL_IDAssignedToInvoice = model.BPL_IDAssignedToInvoice,

                DocumentLines = model.Lines?.Select(line => new SalesOrderLineRequest
                {
                    ItemCode = line.ItemCode,
                    Quantity = line.Quantity,
                    Price = line.Price,
                    WarehouseCode = line.WarehouseCode
                }).ToList() ?? new List<SalesOrderLineRequest>()
            };
        }
    }
}