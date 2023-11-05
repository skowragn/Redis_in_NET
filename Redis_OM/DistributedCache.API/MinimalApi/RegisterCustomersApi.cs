using DistributedCache.Application.Cqrs.Queries;
using MediatR;
using Redis.OM.Contracts;

namespace DistributedCache.API.MinimalApi
{
    public static class CustomersApi
    {
        public static void RegisterCustomersApi(this WebApplication app)
        {
            app.MapGet("/Customers", handler: async (IMediator mediator, IRedisConnectionProvider redisProvider) =>
            {
                var customers = await mediator.Send(new GetAllCustomersQuery());
                return TypedResults.Ok(customers);
            });

        }
    }
}
