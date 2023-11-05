using Redis.OM.Modeling;

namespace DistributedCache.Domain.RedisEntities;

[Document(StorageType = StorageType.Json)]
public class RedisOrganizationEntity
{
    [Indexed] public required int OrgId { get; set; }
    [Indexed] public required string OrgName { get; set; }
    public string? OrgAddress { get; set; }
    public string? OrgEmail { get; set; }
}