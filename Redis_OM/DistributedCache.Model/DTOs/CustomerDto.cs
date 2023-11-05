using Redis.OM.Modeling;

namespace DistributedCache.Model.DTOs;
public class CustomerDto 
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public int Age { get; set; }
    public GeoLoc Home { get; set; }
}