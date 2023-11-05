using DistributedCache.API.Config;
using DistributedCache.Application.Mappers;

namespace DistributedCache.API.Extensions;

public static class RegisterConfiguration
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var applicationSetup = configuration.GetSection(nameof(RedisUsageSecrets)).Get<RedisUsageSecrets>();
        services.AddSingleton(applicationSetup);
        services.AddAutoMapper(typeof(MapperConfig));
        return services;
    }
}