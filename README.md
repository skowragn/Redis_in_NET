# **Redis**
Redis - The open source, in-memory data store used as a database, cache, streaming engine, and message broker.

> [!NOTE]
> There are two versions:
- [x] enterprise
- [x] open source

RedisDB usage
- [x] Caching database
- [x] Primary database

![image](https://github.com/skowragn/redis/assets/97020391/50c3428a-3d2f-4763-bff9-90ef37f532bc) 

_Figure 1: Caching database_

# **Redis Setup**
1. Running Redis server on Windows machine with Windows Subsystem for Linux (WSL2)
   
   https://developer.redis.com/create/windows/
   
   ![image](https://github.com/skowragn/redis/assets/97020391/65ba94c8-58e7-42be-988d-73e9b0942030)
   
3. Running Redis server on docker (recommended for development)
   
    ```docker run -p 6379:6379 -p 8001:8001 redis/redis-stack```

> [!IMPORTANT]
> To use Redis.OM Redis Stack module is needed - very easy to run for development with docker



# **Solution**

The following frameworks/libraries have been used:
    - [x] ASP.NET Core 8 Web Api with minimal Api
    - [x] RedisOM: https://github.com/redis/redis-om-dotnet

## 1. DistributedCache.API 

The Redis usage with the RedisOM (abstract layer for Redis (as primary database or cache) with easy option to query data),

> [!NOTE]
>  It is an abstract layer for the AddStackExchangeRedisCache

Usage with Redis Server and Redis Stack extension setup with docker redis://localhost:6379 for local development


## 2. SimpleDistributedCache.API 
The Azure Cache for Redis is directly usaged as cache (with the AddStackExchangeRedisCache)

# **Redis Tools**
## 1. Redis Insight ![image](https://github.com/skowragn/redis/assets/97020391/d7863bdc-95f7-4228-a552-5811ce62aea6)
  > [!NOTE]
  > More details: https://developer.redis.com/explore/redisinsightv2/windows/

## 2. Redis Stack ![image](https://github.com/skowragn/redis/assets/97020391/2946f912-31a7-4c69-a1b0-4e0253c39a1c)
 > [!NOTE]
>  More details: https://redis.io/docs/about/about-stack/

   
## 3. Redis.OM ![image](https://github.com/skowragn/redis/assets/97020391/284ec89c-23f1-41dd-b7bd-28adac6d539b)

Redis OM is an abstraction for using Redis in .NET, making it easy to model and query your Redis domain objects. 

https://github.com/redis/redis-om-dotnet
  
    
  It contains the following features:
  - [x] Declarative object mapping for Redis objects
  - [x] Declarative secondary-index generation
  - [x] Fluent APIs for querying Redis
  - [x] Fluent APIs for performing Redis aggregations.
