using DistributedCache.Application.Interfaces;
using DistributedCache.Application.Mappers;
using DistributedCache.Domain.Entities;
using DistributedCache.Model.DTOs;
using MediatR;
using DistributedCache.Domain.RedisEntities;

namespace DistributedCache.Application.Cqrs.Queries.Handlers;

internal class GetOrganizationsQueryHandler : IRequestHandler<GetOrganizationsQuery, IEnumerable<OrganizationDto>>
{
    private readonly IOrganizationRepository _organizationsRepository;
    private readonly INoSqlOrganizationsRepository _noSqlOrganizationsRepository;

    public GetOrganizationsQueryHandler(INoSqlOrganizationsRepository noSqlOrganizationsRepository, IOrganizationRepository organizationsRepository)
    {
        _noSqlOrganizationsRepository = noSqlOrganizationsRepository;
        _organizationsRepository = organizationsRepository;
    }

    public async Task<IEnumerable<OrganizationDto>> Handle(GetOrganizationsQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var allRedisOrganizations = await _noSqlOrganizationsRepository.GetAllOrganizations();
        var organizationsDtos = allRedisOrganizations.Select(item => item.ToModel<OrganizationDto>()).ToList();

        if (organizationsDtos.Any())
           return organizationsDtos;
        
        var organizationsDb = await _organizationsRepository.GetAllAsync();
        var enumerable = organizationsDb as Organization[] ?? organizationsDb.ToArray();
        var organizations = enumerable.Select(item => item.ToModel<OrganizationDto>());
        var organizationsToRedis = enumerable.Select(item => item.ToModel<RedisOrganizationEntity>());
        await _noSqlOrganizationsRepository.InsertOrganizations(organizationsToRedis);
            
        return organizations;
    }
}