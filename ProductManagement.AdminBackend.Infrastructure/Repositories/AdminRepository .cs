using Microsoft.EntityFrameworkCore;
using ProductManagement.AdminBackend.Core.Entities;
using ProductManagement.AdminBackend.Core.Interfaces;
using ProductManagement.AdminBackend.Infrastructure.Data;
using System;

namespace ProductManagement.AdminBackend.Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _db;

        public AdminRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Admin admin)
        {
            await _db.Admins.AddAsync(admin);
            await _db.SaveChangesAsync();
        }

        public async Task<Admin?> GetByEmailAsync(string email)
            => await _db.Admins.FirstOrDefaultAsync(x => x.Email == email);

        public async Task<Admin?> GetByIdAsync(Guid id)
            => await _db.Admins.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Admin>> GetAllAsync()
            => await _db.Admins.ToListAsync();

        public async Task UpdateAsync(Admin admin)
        {
            _db.Admins.Update(admin);
            await _db.SaveChangesAsync();
        }
    }
}
