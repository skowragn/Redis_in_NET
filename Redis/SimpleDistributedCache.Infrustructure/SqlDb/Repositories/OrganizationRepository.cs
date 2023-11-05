using SimpleDistributedCache.Domain.Entities;
using SimpleDistributedCache.Application.Interfaces;
using SimpleDistributedCache.Infrastructure.SqlDb.DbContext;

namespace SimpleDistributedCache.Infrastructure.SqlDb.Repositories;

public class OrganizationRepository : Repository<SimpleOrganization>, IOrganizationRepository
{
    public OrganizationRepository(AppDbContext context) : base(context) { }
}