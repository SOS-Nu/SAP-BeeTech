using AutoMapper;
using SAP_BTS_API.Applications.DTOs.responses;
using SAP_BTS_API.domain.models.mappers;
using SAP_BTS_API.Infrastructure.Persistence.SAP_DTOs.SAPresponses;


namespace SAP_BTS_API.Applications.mappings
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<ItemModel, ItemResponseDTO>();

            CreateMap<domain.models.mappers.ItemPrices, Applications.DTOs.responses.ItemPrices>();


            CreateMap<ItemResponseSAP, ItemModel>();
            CreateMap<Infrastructure.Persistence.SAP_DTOs.SAPresponses.ItemPriceSAP, domain.models.mappers.ItemPrices>();



        }

    }
}
