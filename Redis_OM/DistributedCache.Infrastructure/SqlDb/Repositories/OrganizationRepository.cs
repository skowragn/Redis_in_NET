using DistributedCache.Application.Interfaces;
using DistributedCache.Domain.Entities;

namespace DistributedCache.Infrastructure.SqlDb.Repositories;

public class OrganizationRepository : Repository<Organization>, IOrganizationRepository
{
    public OrganizationRepository(AppDbContext context) : base(context) { }
}