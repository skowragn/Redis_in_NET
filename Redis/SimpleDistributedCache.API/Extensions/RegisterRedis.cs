using SimpleDistributedCache.API.Config;
using SimpleDistributedCache.Application.Interfaces;
using SimpleDistributedCache.Infrastructure.NoSql.Cache;

namespace SimpleDistributedCache.API.Extensions;
public static class RegisterRedis 
{
    public static IServiceCollection AddRedisAsDistributedCache(this IServiceCollection services) 
    {
        var serviceProvider = services.BuildServiceProvider();
        var cacheConnectionString = serviceProvider.GetService<ConnectionStrings>()!.AzureRedisUrl;

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = cacheConnectionString;
        });
        
        services.AddTransient<IRedisCache, RedisCacheProvider>();
        
        return services;
    }
}