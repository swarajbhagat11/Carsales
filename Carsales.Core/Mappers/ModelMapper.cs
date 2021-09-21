using AutoMapper;

namespace Carsales.Core.Mappers
{
    public static class ModelMapper
    {
        private static Mapper _mapper;

        static ModelMapper()
        {
            _mapper = new Mapper(MapperConfig.config());
        }

        public static TDestination mapObjects<TDestination>(object obj)
        {
            return _mapper.Map<TDestination>(obj);
        }
    }
}
