using DistributedCache.Domain.RedisEntities;

namespace DistributedCache.Application.Interfaces;

public interface INoSqlCustomersRepository
{
    Task<IEnumerable<RedisCustomerEntity>> GetAllCustomers();
    Task<IEnumerable<RedisCustomerEntity>> GetAllCustomersByLastName(string lastName);
    Task<IEnumerable<RedisCustomerEntity>> GetAllCustomersByLastNameAndAge(string lastName, int age);
    Task<IEnumerable<RedisCustomerEntity>> GetAllCustomersByLastNameAndName(string lastName, string name);
    Task<IEnumerable<RedisCustomerEntity>> GetAllCustomersWithFullNameAndGeoDistance(double longitude, double latitude);
    Task InsertCustomers(IEnumerable<RedisCustomerEntity> customerEntities);
}