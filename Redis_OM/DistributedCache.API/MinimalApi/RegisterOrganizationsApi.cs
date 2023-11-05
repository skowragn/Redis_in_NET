using DistributedCache.Application.Cqrs.Commands;
using DistributedCache.Application.Cqrs.Queries;
using DistributedCache.Application.Interfaces;
using DistributedCache.Domain.Entities;
using MediatR;
using DistributedCache.Model.DTOs;

namespace DistributedCache.API.MinimalApi
{
    public static class OrganizationsApi
    {
        public static void RegisterOrganizationsApi(this WebApplication app)
        {
            app.MapGet("/Organizations", async (IMediator mediator, INoSqlOrganizationsRepository noSqlOrganizationsRepository, IOrganizationRepository organizationsRepository) =>
            {
                var organizations = await mediator.Send(new GetOrganizationsQuery());
                return Results.Ok(organizations);
            });

            app.MapGet("/Organizations/{OrgId:int}", async (IMediator mediator, int orgId, IOrganizationRepository organizationsRepository) =>
            {
                var organization = await mediator.Send(new GetOrganizationByIdQuery(orgId));
                return Results.Ok(organization);
            });

            app.MapPost("/Organizations", async (IMediator mediator, OrganizationDto organization, IOrganizationRepository organizationsRepository) =>
            {
                await mediator.Send(new CreateOrganizationCommand(organization));
                return Results.Created($"/CreateOrganization/{organization.OrgId}", organization);
            });


            app.MapPut("/Organizations/{orgId:int}", async (int orgId, Organization organizationInput, IOrganizationRepository organizationsRepository) =>
            {
                var currentOrganization = await organizationsRepository.GetByIdAsync(orgId);

                currentOrganization.OrgName = organizationInput.OrgName;

                await organizationsRepository.SaveChangesAsync();

                return Results.NoContent();

            });

            app.MapDelete("/Organizations/{orgId:int}", async (int Id, IOrganizationRepository organizationsRepository) =>
            {
                var currentOrganization = await organizationsRepository.GetByIdAsync(Id).ConfigureAwait(false);

                if (currentOrganization == null) return Results.NotFound();
                organizationsRepository.Delete(currentOrganization);
                await organizationsRepository.SaveChangesAsync();
                return Results.Ok(currentOrganization);
            });
        }
    }
}
