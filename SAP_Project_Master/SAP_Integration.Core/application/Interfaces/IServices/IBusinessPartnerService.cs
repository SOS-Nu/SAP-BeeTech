using SAP_Integration.Core.Models;
using SAP_Integration.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP_Integration.Core.Interfaces.Services
{
    public interface IBusinessPartnerService
    {
         BaseResponse<bool> UpdateBusinessPartner(BusinessPartnerModel bpm);
    }
}
