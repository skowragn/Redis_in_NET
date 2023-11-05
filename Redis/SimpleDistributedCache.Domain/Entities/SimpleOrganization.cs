
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleDistributedCache.Domain.Entities;

[Table("SimpleOrganization")]
public class SimpleOrganization : BaseEntity
{
    public required string OrgId { get; set; }

    public required string OrgName { get; set; }

    public string? OrgAddress { get; set; }

    public string? OrgEmail { get; set; }
}