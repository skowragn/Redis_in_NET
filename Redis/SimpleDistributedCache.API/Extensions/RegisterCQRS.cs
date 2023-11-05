using SimpleDistributedCache.Application.Cqrs.Queries;

namespace DistributedCache.API.Extensions;
public static class RegisterCqrs
{
    public static IServiceCollection AddCqrs(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetSimpleOrganizationQuery).Assembly));
        return services;
    }
}