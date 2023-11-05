namespace SimpleDistributedCache.Model.DTOs;

public class OrganizationDto
{
    public required string OrgId { get; set; }

    public required string OrgName { get; set; }

    public string? OrgAddress { get; set; }

    public string? OrgEmail { get; set; }
}