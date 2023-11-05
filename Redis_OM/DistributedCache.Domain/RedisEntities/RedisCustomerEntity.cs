using Redis.OM.Modeling;

namespace DistributedCache.Domain.RedisEntities;

[Document(StorageType = StorageType.Json)]
public class RedisCustomerEntity {
    [Indexed] public string FirstName { get; set; }
    [Indexed] public string LastName { get; set; }
    public string Email { get; set; }
    [Indexed(Aggregatable = true)] public int Age { get; set; }
    [Indexed(Aggregatable = true)] public GeoLoc Home { get; set; }
}