using SAP_DIAPI_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP_DIAPI_Demo.Interfaces.Services
{
    public interface IBusinessPartnerRepository
    {
        BaseResponse<bool> Update(BusinessPartnerModel bp);
    }
}
