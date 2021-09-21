using AutoMapper;
using Carsales.Models;
using Carsales.ViewModels;

namespace Carsales.Core.Mappers
{
    internal static class MapperConfig
    {
        internal static MapperConfiguration config()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<Car, CarDTO>()
                .ForMember(dest => dest.driveAwayPrice,
                   opt => opt.MapFrom(src => src.advertisedPriceType.Equals("DAP") ? src.price : 0))
                .ForMember(dest => dest.excludingGovernmentCharges,
                   opt => opt.MapFrom(src => src.advertisedPriceType.Equals("EGC") ? src.price : 0));
                
                config.CreateMap<CarDTO, Car>()
                .ForMember(dest => dest.price,
                   opt => opt.MapFrom(src => src.advertisedPriceType.Equals("DAP") ? src.driveAwayPrice : src.excludingGovernmentCharges));
            });
        }
    }
}
