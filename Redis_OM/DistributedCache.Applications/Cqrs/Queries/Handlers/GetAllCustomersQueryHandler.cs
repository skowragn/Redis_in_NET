using DistributedCache.Application.Interfaces;
using DistributedCache.Application.Mappers;
using DistributedCache.Domain.RedisEntities;
using DistributedCache.Model.DTOs;
using MediatR;

namespace DistributedCache.Application.Cqrs.Queries.Handlers;

internal class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerDto>>
{
    private readonly INoSqlCustomersRepository _noSqlCustomersRepository;

    public GetAllCustomersQueryHandler(INoSqlCustomersRepository noSqlCustomersRepository)
    {
        _noSqlCustomersRepository = noSqlCustomersRepository;
    }

    public async Task<IEnumerable<CustomerDto>> Handle(GetAllCustomersQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var customers = await _noSqlCustomersRepository.GetAllCustomers();

        if (customers.Any())
        {
            var customersDto = customers.Select(item => item.ToModel<CustomerDto>());
            return customersDto;
        }

        var mockCustomerCollection = GetMockCustomersCollection();
        var redisCustomerEntities = mockCustomerCollection as RedisCustomerEntity[] ?? mockCustomerCollection.ToArray();
        await _noSqlCustomersRepository.InsertCustomers(redisCustomerEntities);
        var mockCustomerDtoCollection = redisCustomerEntities.Select(item => item.ToModel<CustomerDto>()).ToList();

        return mockCustomerDtoCollection;
    }

    private static IEnumerable<RedisCustomerEntity> GetMockCustomersCollection()
    {
        return new List<RedisCustomerEntity>
        {
            new()
            {
                FirstName = "James",
                LastName = "Bond",
                Email = "bondjamesbond@email.com",
                Age = 68
            },
            new()
            {
                FirstName = "John",
                LastName = "Kuklinski",
                Email = "johnkuklinski@email.com",
                Age = 40
            },
            new()
            {
                FirstName = "Anna",
                LastName = "Bond",
                Email = "annabond@email.com",
                Age = 25
            }
        };
    }
}