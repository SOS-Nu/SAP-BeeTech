using SAP_Integration.Core.domain.Models.DTOs;
using SAP_Integration.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP_Integration.Core.domain.models.Mappers
{
    public static class BusinessPartnerMapper
    {
        public static BusinessPartnerRequest ToSLBussinessPartnerDTO(this BusinessPartnerModel model)
        {
            if (model == null) return null;

            return new BusinessPartnerRequest
            {
                CardCode = model.CardCode,
                CardName = model.CardName,
                Phone1 = model.Phone1,
                Email = model.Email,
                FederalTaxID =model.FederalTaxID

            };
        }
   


    }
}
