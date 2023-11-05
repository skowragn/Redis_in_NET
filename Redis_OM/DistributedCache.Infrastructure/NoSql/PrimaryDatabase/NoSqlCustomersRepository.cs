using DistributedCache.Application.Interfaces;
using DistributedCache.Domain.RedisEntities;
using Redis.OM;
using Redis.OM.Aggregation.AggregationPredicates;
using Redis.OM.Contracts;
using Redis.OM.Searching;

namespace DistributedCache.Infrastructure.NoSql.PrimaryDatabase;

public class NoSqlCustomersRepository : INoSqlCustomersRepository
{
    private readonly IRedisConnectionProvider _redisProvider;
    private readonly IRedisConnection _redisConnection;

    public NoSqlCustomersRepository(IRedisConnectionProvider redisProvider)
    {
        ArgumentNullException.ThrowIfNull(redisProvider);
        _redisProvider = redisProvider;
        _redisConnection = _redisProvider.Connection;
    }

    public async Task<IEnumerable<RedisCustomerEntity>> GetAllCustomers()
    {
        var allCustomers = await GetAllCustomerEntities().ConfigureAwait(false);

        return !await allCustomers.AnyAsync() ? new List<RedisCustomerEntity>() : allCustomers;
    }

    public async Task<IEnumerable<RedisCustomerEntity>> GetAllCustomersByLastName(string lastName)
    {
        var allCustomers = await GetAllCustomerEntities().ConfigureAwait(false);

        if (!await allCustomers.AnyAsync()) return new List<RedisCustomerEntity>();
            
        // query
        // Find all customers who's last name is "Bond"
        var matchedCustomers = allCustomers.Where(x => x.LastName == lastName).ToList();

        return matchedCustomers;
    }
    
    public async Task<IEnumerable<RedisCustomerEntity>> GetAllCustomersByLastNameAndAge(string lastName, int age)
    {
        var allCustomers = await GetAllCustomerEntities().ConfigureAwait(false);

        if (!await allCustomers.AnyAsync()) return new List<RedisCustomerEntity>();

        // Find all customers who's last name is Bond OR who's age is greater than 65
        var matchedCustomers = allCustomers.Where(x => x.LastName == lastName || x.Age > age).ToList();

        return matchedCustomers;
    }

    public async Task<IEnumerable<RedisCustomerEntity>> GetAllCustomersByLastNameAndName(string lastName, string name)
    {
        var allCustomers = await GetAllCustomerEntities().ConfigureAwait(false);

        if (!await allCustomers.AnyAsync()) return new List<RedisCustomerEntity>();

        // Find all customer's who's last name is Bond AND who's first name is James
        var matchedCustomers = allCustomers.Where(x => x.LastName == lastName && x.FirstName == name);

        return matchedCustomers;
    }

    public async Task<IEnumerable<RedisCustomerEntity>> GetAllCustomersWithFullNameAndGeoDistance(double longitude, double latitude)
    {
        var allCustomers = await GetAllCustomerEntities().ConfigureAwait(false);

        if (!await allCustomers.AnyAsync()) return new List<RedisCustomerEntity>();

        var customerAggregations = _redisProvider.AggregationSet<RedisCustomerEntity>();

        // Format CustomerDto Full Names
        customerAggregations.Apply(x => string.Format("{0} {1}", x.RecordShell.FirstName, x.RecordShell.LastName), "FullName");

        // Get CustomerDto Distance from Mall of America. 
        customerAggregations.Apply(x => ApplyFunctions.GeoDistance(x.RecordShell.Home, longitude, latitude), "DistanceToMall");

        return allCustomers;
    }

    public async Task InsertCustomers(IEnumerable<RedisCustomerEntity> customerEntities)
    {
        var allCustomers = await GetAllCustomerEntities().ConfigureAwait(false);

        foreach (var item in customerEntities)
        {
            await allCustomers.InsertAsync(item);
        }
    }

    private async Task<IRedisCollection<RedisCustomerEntity>> GetAllCustomerEntities()
    {
        var customers = _redisProvider.RedisCollection<RedisCustomerEntity>();
        // Get Index info
        var indexinfo = await _redisConnection.GetIndexInfoAsync(typeof(RedisCustomerEntity));
        
        // Create index
        if (indexinfo == null)
            await _redisConnection.CreateIndexAsync(typeof(RedisCustomerEntity));
        return customers;
    }
}