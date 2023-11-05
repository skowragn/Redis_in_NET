using Microsoft.EntityFrameworkCore;
using SimpleDistributedCache.API.Config;
using SimpleDistributedCache.Infrastructure.SqlDb.DbContext;
using SimpleDistributedCache.Domain.Entities;

namespace DistributedCache.API.Extensions;
public static class RegisterDatabase 
{
    public static IServiceCollection AddMsSqlDatabase(this IServiceCollection services) 
    { 
        var serviceProvider = services.BuildServiceProvider();
        var connectionString = serviceProvider.GetService<ConnectionStrings>();

        services.AddDbContext<AppDbContext>(options =>
                       options.UseSqlServer(connectionString.MsSqlConnection,
                          opt => opt.MigrationsAssembly(typeof(Program).Assembly.FullName)));

        services.AddDatabaseDeveloperPageExceptionFilter();
        return services;
    }

    public static void InitDatabase(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        using var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

        context.Database.EnsureCreated();


        var defaultOrganization = new SimpleOrganization()
        {
            OrgId = "Number1",
            OrgName = "Default Organization",
            OrgAddress = "Address 1",
            OrgEmail = "test@gmail.com"
        };

     
        if (!context.SimpleOrganization.Any())
        {
            context.SimpleOrganization.Add(defaultOrganization);
        }

        context.SaveChanges();
    }
}