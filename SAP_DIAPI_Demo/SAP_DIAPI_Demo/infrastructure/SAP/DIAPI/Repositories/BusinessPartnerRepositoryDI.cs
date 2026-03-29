using SAP_DIAPI_Demo.Infrastructure;
using SAP_DIAPI_Demo.Interfaces.Services;
using SAP_DIAPI_Demo.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SAP_DIAPI_Demo.Repository
{
    public class BusinessPartnerRepositoryDI : IBusinessPartnerRepository
    {
        public BaseResponse<bool> Update(BusinessPartnerModel bp)
        {
            var company = SapConnector.GetCompany();
            var vBP = (BusinessPartners)company.GetBusinessObject(BoObjectTypes.oBusinessPartners);

            try
            {
                if (!vBP.GetByKey(bp.CardCode))
                    return BaseResponse<bool>.Failure(-404, $"Business Partner {bp.CardCode} not found.");

                if (!string.IsNullOrEmpty(bp.CardName)) vBP.CardName = bp.CardName;
                if (!string.IsNullOrEmpty(bp.Phone1)) vBP.Phone1 = bp.Phone1;
                if (!string.IsNullOrEmpty(bp.Email)) vBP.EmailAddress = bp.Email;
                if (!string.IsNullOrEmpty(bp.FederalTaxID)) vBP.VatIDNum = bp.FederalTaxID;

                int result = vBP.Update();

                if (result != 0)
                {
                    company.GetLastError(out int errCode, out string errMsg);
                    return BaseResponse<bool>.Failure(errCode, errMsg);
                }

                return BaseResponse<bool>.Ok(true);
            }
            finally
            {
                if (vBP != null) Marshal.ReleaseComObject(vBP);
            }
        }
    }
}
