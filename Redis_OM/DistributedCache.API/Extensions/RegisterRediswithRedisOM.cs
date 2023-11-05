using DistributedCache.API.Config;
using Redis.OM;
using Redis.OM.Contracts;

namespace DistributedCache.API.Extensions;
public static class RegisterRedis 
{
    public static IServiceCollection AddRedisWithRedisOM(this IServiceCollection services) 
    {
        var serviceProvider = services.BuildServiceProvider();
        
        var redisConnectionString = serviceProvider.GetService<RedisUsageSecrets>()!.DockerRedisUrl;

        var provider = new RedisConnectionProvider(redisConnectionString);

        services.AddSingleton<IRedisConnectionProvider>(provider);

        return services;
    }
}