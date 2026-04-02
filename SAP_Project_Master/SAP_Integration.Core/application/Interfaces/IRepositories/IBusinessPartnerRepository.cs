using SAP_Integration.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP_Integration.Core.Interfaces.Services
{
    public interface IBusinessPartnerRepository
    {
        BaseResponse<bool> Update(BusinessPartnerModel bp);
    }
}
