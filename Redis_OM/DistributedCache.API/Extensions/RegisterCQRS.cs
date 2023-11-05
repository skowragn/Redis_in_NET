using DistributedCache.Application.Cqrs.Queries;
using DistributedCache.Application.Interfaces;
using DistributedCache.Infrastructure.NoSql.PrimaryDatabase;
using DistributedCache.Infrastructure.SqlDb.Repositories;

namespace DistributedCache.API.Extensions;
public static class RegisterCqrs
{
    public static IServiceCollection AddCqrs(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllCustomersQuery).Assembly));
        return services;
    }
}