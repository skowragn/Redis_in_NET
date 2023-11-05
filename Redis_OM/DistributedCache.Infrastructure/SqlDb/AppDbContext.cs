using DistributedCache.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DistributedCache.Infrastructure.SqlDb;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Product> Products => Set<Product>();

    public DbSet<CustomerOrganization> CustomerOrganization => Set<CustomerOrganization>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Organization>()
                    .HasKey(p => p.Id);

        modelBuilder.Entity<Organization>()
                    .Property(p => p.OrgId)
                    .IsRequired();

        modelBuilder.Entity<Organization>()
                    .Property(p => p.OrgName)
                    .IsRequired();

        modelBuilder.Entity<Organization>()
                    .Property(p => p.OrgAddress);

        modelBuilder.Entity<Organization>()
                    .Property(p => p.OrgEmail);

        modelBuilder.Entity<Customer>()
                    .HasKey(p => p.Id);

        modelBuilder.Entity<Customer>()
                    .Property(p => p.FirstName)
                    .IsRequired();

        modelBuilder.Entity<Customer>()
                    .Property(p => p.LastName)
                    .IsRequired();

        modelBuilder.Entity<Customer>()
                    .Property(p => p.Email)
                    .IsRequired();

        modelBuilder.Entity<Customer>()
                    .Property(p => p.Address);
    
        modelBuilder.Entity<Product>()
                    .HasKey(p => p.Id);
        
        modelBuilder.Entity<Product>()
                    .Property(p => p.Name)
                    .IsRequired();

        modelBuilder.Entity<Product>()
                    .Property(p => p.SerialNumber)
                    .IsRequired();

        modelBuilder.Entity<Product>()
                    .Property(p => p.Price);

        modelBuilder.Entity<Organization>()
                    .Property(p => p.OrgId)
                    .IsRequired();

        modelBuilder.Entity<CustomerOrganization>()
                    .HasKey(p => p.Id);

        modelBuilder.Entity<CustomerOrganization>()
                    .Property(p => p.CustomersId);
        
        modelBuilder.Entity<CustomerOrganization>()
                    .Property(p => p.OrganizationsId);

    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("server=localhost\\SQLEXPRESS; database=Organizations; Integrated Security=true; TrustServerCertificate=True");
        }
    }
}