using DistributedCache.API.Config;
using DistributedCache.Infrastructure.SqlDb;
using Microsoft.EntityFrameworkCore;
using DistributedCache.Domain.Entities;

namespace DistributedCache.API.Extensions;
public static class RegisterDatabase 
{
    public static IServiceCollection AddMsSqlDatabase(this IServiceCollection services) 
    { 
        var serviceProvider = services.BuildServiceProvider();
        var connectionString = serviceProvider.GetService<RedisUsageSecrets>();

        services.AddDbContext<AppDbContext>(
            options =>
            options.UseSqlServer(
                    connectionString.MsSqlConnection,
                    x => x.MigrationsAssembly("DistributedCache.Infrastructure")));
        return services;

    }

    public static void InitDatabase(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        using var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

        context.Database.EnsureCreated();


        var defaultOrganization = new Organization()
        {
            OrgId = "Number1",
            OrgName = "Default Organization",
            OrgAddress = "Address 1",
            OrgEmail = "test@gmail.com"
        };

        var defaultCustomer = new Customer()
        {
            FirstName = "James",
            LastName = "Bond",
            Email = "bondjamesbond@email.com",

        };

        var defaultCustomerOrganization = new CustomerOrganization()
        {
            Customer = defaultCustomer,
            Organization = defaultOrganization,
            CustomersId = defaultCustomer.Id,
            OrganizationsId = defaultOrganization.Id
        };

        var defaultProduct = new Product()
        {
            SerialNumber = "Product1",
            Name = "Product1",
            Price = 10,
        };

        defaultCustomer.Products = new List<Product>() { defaultProduct };
        defaultOrganization.Products = new List<Product>() { defaultProduct };

        if (!context.Organizations.Any())
        {
            context.Organizations.Add(defaultOrganization);
        }

        if (!context.Customers.Any())
        {
            context.Customers.Add(defaultCustomer);
        }

        if (!context.Products.Any())
        {
            context.Products.Add(defaultProduct);
        }
        if (!context.CustomerOrganization.Any())
        {
            context.CustomerOrganization.Add(defaultCustomerOrganization);
        }

        context.SaveChanges();
    }
 }