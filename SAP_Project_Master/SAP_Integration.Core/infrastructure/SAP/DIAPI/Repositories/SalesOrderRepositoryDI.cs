using SAP_Integration.Core.Infrastructure;
using SAP_Integration.Core.Interfaces.Repository;
using SAP_Integration.Core.Models;
using SAP_Integration.Core.Models.Response;
using SAP_Integration.Core.Services;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SAP_Integration.Core.Repositories
{


    public class SalesOrderRepositoryDI : ISalesOrderRepository
    {
        public BaseResponse<SalesOrderResponse> Create(SalesOrderModel order)
        {
            // Inline declaration cho Company và Document
            //hầu hết các chứng từ (Sales Order, Purchase Order, Delivery, Invoice...) đều dùng chung interface  document
            var company = SapConnector.GetCompany();
            var vOrder = (Documents)company.GetBusinessObject(BoObjectTypes.oOrders);

            try
            {
                vOrder.CardCode = order.CardCode;
                vOrder.DocDueDate = order.DocDueDate;
                vOrder.Comments = order.Comments;
                vOrder.BPL_IDAssignedToInvoice = order.BPL_IDAssignedToInvoice;
                

                for (int i = 0; i < order.Lines.Count; i++)
                {
                    //dòng line 0 ko cần add nó tự thêm vì là con trỏ hiện t
                    if (i > 0) vOrder.Lines.Add();

                    var lineData = order.Lines[i]; 
                    vOrder.Lines.ItemCode = lineData.ItemCode;
                    vOrder.Lines.Quantity = lineData.Quantity;
                    

                    if (lineData.Price > 0) vOrder.Lines.Price = lineData.Price;
                }

                if (vOrder.Add() != 0)
                {
                    company.GetLastError(out int errorCode, out string errorMsg);
                    //này là tự tạo mới 1 object
                    //return new BaseResponse<SalesOrderResponse> { Success = false, Message = errorMsg, ErrorCode = errorCode };
                    return BaseResponse<SalesOrderResponse>.Failure(errorCode, errorMsg);
                }

                int newEntry = int.Parse(company.GetNewObjectKey());

                if (vOrder.GetByKey(newEntry))
                {

                    var fullData = new SalesOrderResponse
                    {
                        DocEntry = vOrder.DocEntry,
                        DocNum = vOrder.DocNum,
                        CardCode = vOrder.CardCode,
                        CardName = vOrder.CardName,
                        DocTotal = vOrder.DocTotal,
                        Currency = vOrder.DocCurrency
                    };

                    return BaseResponse<SalesOrderResponse>.Ok(fullData);
                }
                else
                {
                    return BaseResponse<SalesOrderResponse>.Failure();

                }
            }

            finally
            {
                if (vOrder != null) Marshal.ReleaseComObject(vOrder);
            }
        }
    }
}
