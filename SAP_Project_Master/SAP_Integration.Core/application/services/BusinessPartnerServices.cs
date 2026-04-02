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