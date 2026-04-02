using SAP_Integration.Core.Interfaces.Repository;
using SAP_Integration.Core.Interfaces.Services;
using SAP_Integration.Core.Models;
using SAP_Integration.Core.Models.Response;
using SAP_Integration.Core.Repositories;
using SAPbobsCOM;
using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;


namespace SAP_Integration.Core.Services
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

            return _repository.UpdatePrice(item);
        }
    }
}