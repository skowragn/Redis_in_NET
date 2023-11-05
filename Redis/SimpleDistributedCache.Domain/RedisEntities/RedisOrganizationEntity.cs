namespace SimpleDistributedCache.Domain.RedisEntities;

public class RedisOrganizationEntity
{
    public required string OrgId { get; set; }

    public required string OrgName { get; set; }

    public string? OrgAddress { get; set; }

    public string? OrgEmail { get; set; }
}