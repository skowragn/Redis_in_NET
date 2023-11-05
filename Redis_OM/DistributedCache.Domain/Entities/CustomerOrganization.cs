using System.ComponentModel.DataAnnotations.Schema;

namespace DistributedCache.Domain.Entities;

[Table("CustomerOrganization")]
public class CustomerOrganization : BaseEntity
{
    public int CustomersId { get; set; }
    public int OrganizationsId { get; set; }
    public Customer Customer { get; set; } = null!;
    public Organization Organization { get; set; } = null!;
}
