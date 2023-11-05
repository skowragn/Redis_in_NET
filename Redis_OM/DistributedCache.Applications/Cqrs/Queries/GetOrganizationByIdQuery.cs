using DistributedCache.Model.DTOs;
using MediatR;

namespace DistributedCache.Application.Cqrs.Queries;

public class GetOrganizationByIdQuery : IRequest<IEnumerable<OrganizationDto>>
{
    public GetOrganizationByIdQuery(int orgId)
    {
        OrgId = orgId;
    }

    public int OrgId { get; }
}