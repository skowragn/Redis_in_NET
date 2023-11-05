using SimpleDistributedCache.Model.DTOs;
using MediatR;
using SimpleDistributedCache.Application.Interfaces;
using SimpleDistributedCache.Application.Mappers;
using SimpleDistributedCache.Domain.RedisEntities;
using SimpleDistributedCache.Domain.Entities;

namespace SimpleDistributedCache.Application.Cqrs.Queries.Handlers;

internal class GetSimpleOrganizationsQueryHandler : IRequestHandler<GetSimpleOrganizationQuery, IEnumerable<OrganizationDto>>
{
    private readonly IOrganizationRepository _organizationsRepository;
    private readonly IRedisCache _redisCache;

    public GetSimpleOrganizationsQueryHandler(IRedisCache redisCache, IOrganizationRepository organizationsRepository)
    {
        ArgumentNullException.ThrowIfNull(redisCache);
        ArgumentNullException.ThrowIfNull(organizationsRepository);
        _organizationsRepository = organizationsRepository;
        _redisCache = redisCache;
    }

    public async Task<IEnumerable<OrganizationDto>> Handle(GetSimpleOrganizationQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        const string cacheKey = "organizations";
        const double absoluteExpirationRelativeToNow = 10.0;
        const double slidingExpiration = 5.0;

        var redisCacheOrganizationsList = await _redisCache.GetCacheDataAsync<List<RedisOrganizationEntity>>(cacheKey);

        if (redisCacheOrganizationsList != null)
          return redisCacheOrganizationsList.Select(item => item.ToModel<OrganizationDto>()).ToList();
        
        var organizationsDb = await _organizationsRepository.GetAllAsync();
        var enumerable = organizationsDb as SimpleOrganization[] ?? organizationsDb.ToArray();
        var organizationsToRedis = enumerable.Select(item => item.ToModel<RedisOrganizationEntity>()).ToList();
        try 
        {
            await _redisCache.SetCacheDataAsync(cacheKey, organizationsToRedis, absoluteExpirationRelativeToNow, slidingExpiration);
        }
        catch (Exception ex)
        {
            var log = ex.Message;
        }

        return enumerable.Select(item => item.ToModel<OrganizationDto>());
    }
}