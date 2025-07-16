using AutoMapper;
using BLL.DTOs;
using DAL.Entities;

namespace BLL.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Orchid, OrchidDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));

            CreateMap<OrchidDTO, Orchid>()
                .ForMember(dest => dest.Category, opt => opt.Ignore()); // avoid circular reference
        }
    }
}

