using SAP_DIAPI_Demo.Interfaces.Repository;
using SAP_DIAPI_Demo.Interfaces.Services;
using SAP_DIAPI_Demo.Models;
using SAP_DIAPI_Demo.Models.Response;
using SAP_DIAPI_Demo.Repositories;
using SAPbobsCOM;
using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;


namespace SAP_DIAPI_Demo.Services
{


    public class SalesOrderService : ISalesOrderService
    {
        private readonly ISalesOrderRepository _repository; 
        public SalesOrderService(ISalesOrderRepository repository)
        {
            _repository = repository;
        }
         

        public BaseResponse<SalesOrderResponse> CreateOrder(SalesOrderModel order)
        {
            if (string.IsNullOrEmpty(order.CardCode))
                return  BaseResponse<SalesOrderResponse>.Failure(-1,"Customer Code is required." );

            if (order.Lines == null || order.Lines.Count == 0)
                return  BaseResponse<SalesOrderResponse>.Failure(-1, "Order must have at least one line.");

            return _repository.Create(order);
        }
    }
}