using SimpleDistributedCache.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SimpleDistributedCache.Infrastructure.SqlDb.DbContext;

public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<SimpleOrganization> SimpleOrganization => Set<SimpleOrganization>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SimpleOrganization>()
                .HasKey(p => p.Id);

        modelBuilder.Entity<SimpleOrganization>()
            .HasKey(p => p.OrgId);

        modelBuilder.Entity<SimpleOrganization>()
               .Property(p => p.OrgName)
               .IsRequired();

        modelBuilder.Entity<SimpleOrganization>()
                    .Property(p => p.OrgAddress);

        modelBuilder.Entity<SimpleOrganization>()
                    .Property(p => p.OrgEmail);

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("server=localhost\\SQLEXPRESS; database=SimpleOrganizations; Integrated Security=true; TrustServerCertificate=True");
        }
    }
}