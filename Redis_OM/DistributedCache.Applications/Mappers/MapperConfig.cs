using AutoMapper;
using DistributedCache.Domain.Entities;
using DistributedCache.Model.DTOs;
using DistributedCache.Domain.RedisEntities;

namespace DistributedCache.Application.Mappers;
public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<Organization, OrganizationDto>().ReverseMap();
        CreateMap<RedisOrganizationEntity, Organization>().ReverseMap();
        CreateMap<RedisOrganizationEntity, OrganizationDto>().ReverseMap();
        CreateMap<CustomerDto, RedisCustomerEntity>().ReverseMap(); 
 }
}