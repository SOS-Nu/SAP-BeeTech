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


    public class BusinessPartnerService : IBusinessPartnerService
    {
        private readonly IBusinessPartnerRepository _repository; 
        public BusinessPartnerService(IBusinessPartnerRepository repository)
        {
            _repository = repository;
        }


        public BaseResponse<bool> UpdateBusinessPartner(BusinessPartnerModel bpm)
        {
            if (string.IsNullOrEmpty(bpm.CardCode))
                return BaseResponse<bool>.Failure(-1, "Customer Code is required.");
            return _repository.Update(bpm);
        }
    }
}