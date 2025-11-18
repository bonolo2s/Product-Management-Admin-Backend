using ProductManagement.AdminBackend.Core.DTOs;
using ProductManagement.AdminBackend.Core.Entities;

namespace ProductManagement.AdminBackend.Core.Interfaces
{
    public interface IAdminService
    {
        Task<Admin> CreateAdminAsync(CreateAdminDto dto, Guid createdBy);
        Task<Admin?> GetAdminByIdAsync(Guid id);
        Task<IEnumerable<Admin>> GetAllAdminsAsync();
        Task<Admin> UpdateAdminAsync(Guid id, UpdateAdminDto dto, Guid updatedBy);
        Task<bool> DisableAdminAsync(Guid id, Guid disabledBy);
        Task<bool> ResetPasswordAsync(Guid id, Guid resetByAdminId);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
    }
}
