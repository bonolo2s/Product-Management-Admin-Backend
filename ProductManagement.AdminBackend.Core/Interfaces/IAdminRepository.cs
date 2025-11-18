using ProductManagement.AdminBackend.Core.Entities;

namespace ProductManagement.AdminBackend.Core.Interfaces
{
    public interface IAdminRepository
    {
        Task<Admin?> GetByIdAsync(Guid id);
        Task<Admin?> GetByEmailAsync(string email);
        Task<IEnumerable<Admin>> GetAllAsync();
        Task CreateAsync(Admin admin);
        Task UpdateAsync(Admin admin);
    }
}
