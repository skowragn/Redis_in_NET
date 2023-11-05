using DistributedCache.Domain.RedisEntities;

namespace DistributedCache.Application.Interfaces;

public interface INoSqlOrganizationsRepository
{
    Task<IEnumerable<RedisOrganizationEntity>> GetAllOrganizations();

    Task<IEnumerable<RedisOrganizationEntity>> GetAllOrganizationsByOrgId(int orgId);

    Task InsertOrganizations(IEnumerable<RedisOrganizationEntity> organizationsToStore);
}