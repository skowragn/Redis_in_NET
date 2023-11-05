using DistributedCache.Application.Interfaces;
using DistributedCache.Infrastructure.NoSql.PrimaryDatabase;
using DistributedCache.Infrastructure.SqlDb.Repositories;

namespace DistributedCache.API.Extensions;

public static class RegisterAppServices
{
    public static IServiceCollection AddExtraServices(this IServiceCollection services)
    {
        services.AddTransient<INoSqlOrganizationsRepository, NoSqlOrganizationsRepository>();
        services.AddTransient<IOrganizationRepository, OrganizationRepository>();
        services.AddTransient<INoSqlCustomersRepository, NoSqlCustomersRepository>();

        return services;
    }
}