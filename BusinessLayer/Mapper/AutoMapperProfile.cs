using AutoMapper;
using Products_Microservice.DTO;
using Products_Microservice.Models;

namespace Products_Microservice.BusinessLayer.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductsDTO, Products>();
            CreateMap<ProductsDTO, Products>().ReverseMap();
        }
    }
}
