using System.ComponentModel.DataAnnotations.Schema;

namespace DistributedCache.Domain.Entities;

[Table("Customers")]
public class Customer : BaseEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? Address { get; set; }

    // one-to-many Customer -> Product
    public ICollection<Product>? Products { get; set; }
    
    // many-to-many Organization -> Customer
    public List<CustomerOrganization> CustomerOrganization { get; } = new();
}
