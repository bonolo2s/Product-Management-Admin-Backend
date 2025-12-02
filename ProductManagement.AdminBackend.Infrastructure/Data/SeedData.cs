using ProductManagement.AdminBackend.Core.Entities;
using ProductManagement.AdminBackend.Core.Enums;
using ProductManagement.AdminBackend.Infrastructure.Data;

public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        if (!context.Admins.Any())
        {
            var superAdmin = new Admin
            {
                Id = Guid.NewGuid(),
                FullName = "System SuperAdmin",
                Email = "superadmin@domain.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("SuperAdmin123!"),
                adminRole = AdminRole.SuperAdmin,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            context.Admins.Add(superAdmin);
            context.SaveChanges();
        }
    }
}
