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


    public class ItemService : IItemService
    {
        private readonly IItemRepository _repository; 
        public ItemService(IItemRepository repository)
        {
            _repository = repository;
        }
         

        public BaseResponse<bool> UpdatePriceItem(ItemModel item)
        {
            if (string.IsNullOrEmpty(item.ItemCode))
                return  BaseResponse<bool>.Failure(-1, "ItemCode is required.");

           ;

            return _repository.UpdatePriceDI(item);
        }
    }
}