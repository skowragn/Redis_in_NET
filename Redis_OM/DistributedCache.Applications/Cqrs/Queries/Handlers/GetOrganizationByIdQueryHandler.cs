using DistributedCache.Application.Interfaces;
using DistributedCache.Application.Mappers;
using DistributedCache.Model.DTOs;
using MediatR;
using Redis.OM.Contracts;

namespace DistributedCache.Application.Cqrs.Queries.Handlers;

internal class GetOrganizationByIdQueryHandler : IRequestHandler<GetOrganizationByIdQuery, IEnumerable<OrganizationDto>>
{
    private readonly IOrganizationRepository _organizationsRepository;
    private readonly INoSqlOrganizationsRepository _noSqlOrganizationsRepository;

    public GetOrganizationByIdQueryHandler(INoSqlOrganizationsRepository noSqlOrganizationsRepository, IOrganizationRepository organizationsRepository)
    {
        ArgumentNullException.ThrowIfNull(noSqlOrganizationsRepository);
        ArgumentNullException.ThrowIfNull(organizationsRepository);
        _noSqlOrganizationsRepository = noSqlOrganizationsRepository;
        _organizationsRepository = organizationsRepository;
    }
    public async Task<IEnumerable<OrganizationDto>> Handle(GetOrganizationByIdQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var organization = await _noSqlOrganizationsRepository.GetAllOrganizationsByOrgId(request.OrgId);
        var organizationDto = organization.Select(item => item.ToModel<OrganizationDto>()).ToList();
        return organizationDto;
    }
}