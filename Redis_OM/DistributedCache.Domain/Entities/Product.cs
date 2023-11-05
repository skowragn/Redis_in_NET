using System.ComponentModel.DataAnnotations.Schema;

namespace DistributedCache.Domain.Entities;

[Table("Product")]
public class Product : BaseEntity
{
    public required string SerialNumber { get; set; }

    public required string Name { get; set; }

    public decimal Price { get; set; }

    // one-to-many Organization -> Product
    public int ProductOrganizationId { get; set; }
   
    public Organization Organization { get; set; } = null!;

    // one-to-many Customer -> Product
    public int ProductCustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

}