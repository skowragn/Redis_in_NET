namespace SimpleDistributedCache.Application.Interfaces;

public interface IRedisCache {
    Task<T?> GetCacheDataAsync<T>(string cacheKey);
    Task RemoveCacheDataAsync(string cacheKey);
    Task SetCacheDataAsync<T>(string cacheKey, T cacheValue, double absExpRelToNow, double slidingExpiration);
}