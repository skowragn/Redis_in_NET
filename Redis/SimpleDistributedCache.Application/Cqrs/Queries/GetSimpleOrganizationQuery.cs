using SimpleDistributedCache.Model.DTOs;
using MediatR;

namespace SimpleDistributedCache.Application.Cqrs.Queries;

public class GetSimpleOrganizationQuery : IRequest<IEnumerable<OrganizationDto>>
{
}