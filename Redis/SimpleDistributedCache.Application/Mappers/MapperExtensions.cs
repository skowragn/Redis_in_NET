using AutoMapper;

namespace SimpleDistributedCache.Application.Mappers;

public static class MapperExtensions 
{
    static MapperExtensions() {
        Mapper = new MapperConfiguration(cfg => cfg.AddProfile<MapperConfig>())
            .CreateMapper();
    }

    internal static IMapper Mapper { get; }

    public static T ToModel<T>(this object source) {
        return Mapper.Map<T>(source);
    }
}