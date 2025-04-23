using AutoMapper;
using CalorieCalculator.Dtos;
using CalorieCalculator.Models;

namespace CalorieCalculatorCore.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Dto
            CreateMap<MenuItem, MenuItemDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<MeasureType, MeasureTypeDto>();
            CreateMap<Menu, MenuDto>();

            // Dto to Domain
            CreateMap<MenuItemDto, MenuItem>();
            CreateMap<MenuDto, Menu>();

            CreateMap<EdamamProductDto, Product>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src.Parsed[0].Food.Name)
                )
                .ForMember(
                    dest => dest.FoodId,
                    opt => opt.MapFrom(src => src.Parsed[0].Food.FoodId)
                )

                .ForMember(
                    dest => dest.Energy,
                    opt => opt.MapFrom(src => src.Parsed[0].Food.Nutrients.Energy)
                )
                .ForMember(
                    dest => dest.Carbs,
                    opt => opt.MapFrom(src => src.Parsed[0].Food.Nutrients.Carbs)
                )
                .ForMember(
                    dest => dest.Protein,
                    opt => opt.MapFrom(src => src.Parsed[0].Food.Nutrients.Protein)
                )
                .ForMember(
                    dest => dest.Fat,
                    opt => opt.MapFrom(src => src.Parsed[0].Food.Nutrients.Fat)
                )
                .ForMember(
                    dest => dest.Fiber,
                    opt => opt.MapFrom(src => src.Parsed[0].Food.Nutrients.Fiber)
                );
        }
    }
}