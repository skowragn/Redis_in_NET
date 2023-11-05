using DistributedCache.Model.DTOs;
using MediatR;

namespace DistributedCache.Application.Cqrs.Commands;

public class CreateOrganizationCommand : IRequest<string>
{
    public CreateOrganizationCommand(OrganizationDto organization)
    {
        Organization = organization;
    }

    public OrganizationDto Organization { get; }
}