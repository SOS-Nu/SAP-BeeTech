using SAP_DIAPI_Demo.Models;
using SAP_DIAPI_Demo.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP_DIAPI_Demo.Interfaces.Services
{
    public interface IBusinessPartnerService
    {
         BaseResponse<bool> UpdateBusinessPartner(BusinessPartnerModel bpm);
    }
}
