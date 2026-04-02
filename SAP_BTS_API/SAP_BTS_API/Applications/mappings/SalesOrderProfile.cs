using AutoMapper;
using SAP_BTS_API.Applications.DTOs.requests;
using SAP_BTS_API.Applications.DTOs.responses;
using SAP_BTS_API.domain.models.mappers; // Lưu ý: Namespace nên đặt là .Domain.Models
using SAP_BTS_API.Infrastructure.Persistence.SAP_DTOs.SAPrequests;
using SAP_BTS_API.Infrastructure.Persistence.SAP_DTOs.SAPresponses;

namespace SAP_BTS_API.Applications.mappings
{
    public class SalesOrderProfile : Profile
    {
        public SalesOrderProfile()
        {

            // client req => model
            CreateMap<SalesOrderRequestDTO, SalesOrderModel>()
                .ForMember(dest => dest.Lines, opt => opt.MapFrom(src => src.DocumentLines))
                .ForMember(dest => dest.DocDueDate, opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.DocDueDate) ? DateTime.Now : DateTime.Parse(src.DocDueDate)));

            CreateMap<Applications.DTOs.requests.DocumentLines, SalesOrderItemModel>();


            // model => reqSAP (Gửi lên SAP Service Layer)
            CreateMap<SalesOrderModel, SalesOrderRequestSAP>()
                .ForMember(dest => dest.DocDueDate, opt => opt.MapFrom(src => src.DocDueDate.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.DocumentLines, opt => opt.MapFrom(src => src.Lines));

            CreateMap<SalesOrderItemModel, Infrastructure.Persistence.SAP_DTOs.SAPrequests.DocumentLines>();


            //Reponse

            // resSAP => model
            CreateMap<SalesOrderResponseSAP, SalesOrderModel>()
                .ForMember(dest => dest.Lines, opt => opt.MapFrom(src => src.DocumentLines))
                .ForMember(dest => dest.DocDueDate, opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.DocDueDate) ? DateTime.Now : DateTime.Parse(src.DocDueDate)));

            CreateMap<SalesOrderLineResponseSAP, SalesOrderItemModel>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.UnitPrice));


            // model => resclient
            CreateMap<SalesOrderModel, SalesOrderResponseDTO>()
                    .ForMember(dest => dest.DocumentLines, opt => opt.MapFrom(src => src.Lines));
       
            CreateMap<SalesOrderItemModel, SalesOrderResponseDTO.SalesOrderLineResponseDTO>();
        }
    }
}