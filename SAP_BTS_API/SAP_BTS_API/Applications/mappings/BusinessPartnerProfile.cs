using AutoMapper;
using SAP_BTS_API.Applications.DTOs.requests;
using SAP_BTS_API.Applications.DTOs.responses;
using SAP_BTS_API.domain.models.mappers;
using SAP_BTS_API.Infrastructure.Persistence.SAP_DTOs.SAPresponses;


namespace SAP_BTS_API.Applications.mappings
{
    public class BusinessPartnerProfile : Profile
    {
        public BusinessPartnerProfile()
        {

            //map model => resdto
            CreateMap<BusinessPartnerModel, BusinessPartnerResponseDTO>()
                 .ForMember(dest => dest.CurrentBalance, opt => opt.MapFrom(src => src.Balance));
            //map ressap => model
            CreateMap<BusinessPartnerResponseSAP, BusinessPartnerModel>();


        }
    }
}