using SimpleDistributedCache.Application.Interfaces;
using SimpleDistributedCache.Infrastructure.SqlDb.Repositories;

namespace DistributedCache.API.Extensions;

public static class RegisterAppServices
{
    public static IServiceCollection AddExtraServices(this IServiceCollection services)
    {
        services.AddTransient<IOrganizationRepository, OrganizationRepository>();
        return services;
    }
}