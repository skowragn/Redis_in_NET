using SimpleDistributedCache.API.Config;
using SimpleDistributedCache.Application.Mappers;

namespace DistributedCache.API.Extensions;

public static class RegisterConfiguration
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var applicationSetup = configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>();
        services.AddSingleton(applicationSetup);
        services.AddAutoMapper(typeof(MapperConfig));
        return services;
    }
}