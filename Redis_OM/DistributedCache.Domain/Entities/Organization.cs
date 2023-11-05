using System.ComponentModel.DataAnnotations.Schema;
namespace DistributedCache.Domain.Entities;

[Table("Organization")]
public class Organization : BaseEntity
{
    public required string OrgId { get; set; }

    public required string OrgName { get; set; }
    
    public string? OrgAddress { get; set; }

    public string? OrgEmail { get; set; }

    // one-to-many Organization -> Product
    public ICollection<Product>? Products { get; set; }

    // many-to-many Organization -> Customer
    public List<CustomerOrganization> CustomerOrganization { get; } = new();
}