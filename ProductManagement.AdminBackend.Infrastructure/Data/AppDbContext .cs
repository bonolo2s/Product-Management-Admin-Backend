using Microsoft.EntityFrameworkCore;
using ProductManagement.AdminBackend.Core.Entities;

namespace ProductManagement.AdminBackend.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Admin> Admins => Set<Admin>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    }
}
