using DistributedCache.Application.Interfaces;
using DistributedCache.Domain.RedisEntities;
using Redis.OM;
using Redis.OM.Contracts;
using Redis.OM.Searching;

namespace DistributedCache.Infrastructure.NoSql.PrimaryDatabase;

public class NoSqlOrganizationsRepository : INoSqlOrganizationsRepository
{
    private readonly IRedisConnectionProvider _redisProvider;
    private readonly IRedisConnection _redisConnection;

    public NoSqlOrganizationsRepository(IRedisConnectionProvider redisProvider)
    {
        _redisProvider = redisProvider;
        _redisConnection = _redisProvider.Connection;
    }
    public async Task<IEnumerable<RedisOrganizationEntity>> GetAllOrganizations()
    { 
        var organizations = _redisProvider.RedisCollection<RedisOrganizationEntity>();
        var indexinfo = _redisConnection.GetIndexInfo(typeof(RedisOrganizationEntity));

        _redisConnection.CreateIndex(typeof(RedisOrganizationEntity));

       var allCachedOrganizations = await organizations.ToListAsync();

       return allCachedOrganizations;
    }

    public async Task<IEnumerable<RedisOrganizationEntity>> GetAllOrganizationsByOrgId(int orgId)
    {
        var allOrganizations = await GetAllOrganizationsEntities().ConfigureAwait(false);

        if (!await allOrganizations.AnyAsync()) return new List<RedisOrganizationEntity>(); 
 
        var matchedOrganizations = allOrganizations.Where(x => x.OrgId == orgId).ToList();

        return matchedOrganizations;
    }

  
    public async Task InsertOrganizations(IEnumerable<RedisOrganizationEntity> organizationsToStore)
    {
        var allOrganizations = await GetAllOrganizationsEntities().ConfigureAwait(false);

        foreach (var item in organizationsToStore)
        {
            await allOrganizations.InsertAsync(item);
        }
    }

    private async Task<IRedisCollection<RedisOrganizationEntity>> GetAllOrganizationsEntities()
    {
        IRedisCollection<RedisOrganizationEntity> organizations = _redisProvider.RedisCollection<RedisOrganizationEntity>();
        
        // Get Index info
        var indexinfo = _redisConnection.GetIndexInfo(typeof(RedisOrganizationEntity));

        // Create index
        if (indexinfo == null)
            _redisConnection.CreateIndex(typeof(RedisOrganizationEntity));

        return organizations;
    }
}