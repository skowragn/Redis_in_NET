using AutoMapper;
using SimpleDistributedCache.Domain.Entities;
using SimpleDistributedCache.Domain.RedisEntities;
using SimpleDistributedCache.Model.DTOs;

namespace SimpleDistributedCache.Application.Mappers;
public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<SimpleOrganization, OrganizationDto>().ReverseMap();
        CreateMap<RedisOrganizationEntity, SimpleOrganization>().ReverseMap();
        CreateMap<RedisOrganizationEntity, OrganizationDto>().ReverseMap();
    }
}