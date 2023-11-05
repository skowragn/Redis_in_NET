using MediatR;
using SimpleDistributedCache.Application.Cqrs.Queries;
using SimpleDistributedCache.Application.Interfaces;

namespace SimpleDistributedCache.API.MinimalApi
{
    public static class OrganizationsApi
    {
        public static void RegisterOrganizationsApi(this WebApplication app)
        {
            app.MapGet("/Organizations", async (IMediator mediator,IRedisCache redisCache, IOrganizationRepository organizationsRepository) =>
            {
                var organizations = await mediator.Send(new GetSimpleOrganizationQuery());
                return Results.Ok(organizations);
            });
        }
    }
}
