using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using SimpleDistributedCache.Application.Interfaces;

namespace SimpleDistributedCache.Infrastructure.NoSql.Cache;

public class RedisCacheProvider : IRedisCache
{
    private readonly IDistributedCache _redis;
    
    public RedisCacheProvider(IDistributedCache redis) 
    {
        _redis = redis;
    }

    public IDistributedCache Redis => _redis;

    public async Task<T?> GetCacheDataAsync<T>(string cacheKey) 
    {
        try
        {
            var cacheData = await Redis.GetStringAsync(cacheKey);
            return !string.IsNullOrEmpty(cacheData) ? JsonSerializer.Deserialize<T>(cacheData) : default;
        }
        catch (Exception ex)
        {
            var log = ex.Message;
            return default;
        }
       
    }

    public async Task RemoveCacheDataAsync(string cacheKey) 
    {
        await Redis.RemoveAsync(cacheKey);
    }

    public async Task SetCacheDataAsync<T>(string cacheKey, T cacheValue, double absExpRelToNow = 10.0, double slidingExpiration = 5.0) {

        var cacheExpiry = new DistributedCacheEntryOptions 
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(absExpRelToNow),
            SlidingExpiration = TimeSpan.FromMinutes(slidingExpiration)
        };

        await Redis.SetStringAsync(cacheKey, JsonSerializer.Serialize(cacheValue), cacheExpiry);
    }

}