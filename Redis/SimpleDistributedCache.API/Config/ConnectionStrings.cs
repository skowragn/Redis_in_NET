namespace SimpleDistributedCache.API.Config;
public class ConnectionStrings 
{ 
    public string? MsSqlConnection { get; set; }
    public string? AzureRedisUrl { get; set; }
}