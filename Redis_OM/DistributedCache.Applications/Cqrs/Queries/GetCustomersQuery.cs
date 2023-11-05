using DistributedCache.Model.DTOs;
using MediatR;

namespace DistributedCache.Application.Cqrs.Queries;

public class GetAllCustomersQuery : IRequest<IEnumerable<CustomerDto>>
{

}