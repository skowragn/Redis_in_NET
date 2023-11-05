using DistributedCache.Application.Interfaces;
using DistributedCache.Application.Mappers;
using DistributedCache.Domain.Entities;
using MediatR;

namespace DistributedCache.Application.Cqrs.Commands.Handlers;

internal class CreateGroupCommandHandler : IRequestHandler<CreateOrganizationCommand, string>
{
    private readonly IOrganizationRepository _organizationRepository;

    public CreateGroupCommandHandler(IOrganizationRepository organizationRepository)
    {
        ArgumentNullException.ThrowIfNull(organizationRepository, nameof(organizationRepository));
        _organizationRepository = organizationRepository;
    }

    public async Task<string> Handle(
        CreateOrganizationCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(request.Organization);

        var organization = request.Organization.ToModel<Organization>();

        _organizationRepository.Add(organization);
         var result = await _organizationRepository.SaveChangesAsync();
         return result.ToString();
    }
}