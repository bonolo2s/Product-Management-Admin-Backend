using ProductManagement.AdminBackend.Core.Entities;
using ProductManagement.AdminBackend.Core.Enums;
using ProductManagement.AdminBackend.Core.Interfaces;
using ProductManagement.AdminBackend.Core.DTOs;

namespace ProductManagement.AdminBackend.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repo;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly IAuditLogger _audit;

        public AdminService(
            IAdminRepository repo,
            IPasswordHasher passwordHasher,
            ITokenService tokenService,
            IAuditLogger audit)
        {
            _repo = repo;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _audit = audit;
        }

        public async Task<Admin> CreateAdminAsync(CreateAdminDto dto, Guid createdBy)
        {
            var existing = await _repo.GetByEmailAsync(dto.Email);
            if (existing != null) throw new Exception("Admin with this email already exists.");

            var admin = new Admin
            {
                Id = Guid.NewGuid(),
                FullName = dto.FullName,
                Email = dto.Email,
                Role = AdminRole.Admin.ToString(),
                PasswordHash = _passwordHasher.Hash(dto.Password)
            };

            await _repo.CreateAsync(admin);

            await _audit.LogAsync(AuditEvent.AdminCreated, createdBy, admin.Id);

            return admin;
        }

        public async Task<Admin?> GetAdminByIdAsync(Guid id)
            => await _repo.GetByIdAsync(id);

        public async Task<IEnumerable<Admin>> GetAllAdminsAsync()
            => await _repo.GetAllAsync();

        public async Task<Admin> UpdateAdminAsync(Guid id, UpdateAdminDto dto, Guid updatedBy)
        {
            var admin = await _repo.GetByIdAsync(id);
            if (admin == null) throw new Exception("Admin not found.");

            admin.FullName = dto.FullName;
            admin.Email = dto.Email;
            admin.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(admin);

            await _audit.LogAsync(AuditEvent.AdminUpdated, updatedBy, admin.Id);

            return admin;
        }

        public async Task<bool> DisableAdminAsync(Guid id, Guid disabledBy)
        {
            var admin = await _repo.GetByIdAsync(id);
            if (admin == null) return false;

            admin.IsActive = false;
            await _repo.UpdateAsync(admin);

            await _audit.LogAsync(AuditEvent.AdminDeleted, disabledBy, admin.Id);

            return true;
        }

        public async Task<bool> ResetPasswordAsync(Guid id, Guid resetBy)
        {
            var admin = await _repo.GetByIdAsync(id);
            if (admin == null) return false;

            var newPassword = Guid.NewGuid().ToString("N").Substring(0, 10);
            admin.PasswordHash = _passwordHasher.Hash(newPassword);

            await _repo.UpdateAsync(admin);

            await _audit.LogAsync(AuditEvent.AdminUpdated, resetBy, admin.Id);

            // send email in Infrastructure

            return true;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var admin = await _repo.GetByEmailAsync(dto.Email);
            if (admin == null) throw new Exception("Invalid credentials.");

            if (!_passwordHasher.Verify(admin.PasswordHash, dto.Password))
            {
                await _audit.LogAsync(AuditEvent.LoginFailed, Guid.Empty, admin?.Id ?? Guid.Empty);
                throw new Exception("Invalid credentials.");
            }

            if (!admin.IsActive) throw new Exception("Account disabled.");

            var tokens = _tokenService.GenerateTokens(admin);

            await _audit.LogAsync(AuditEvent.LoginSuccess, admin.Id, admin.Id);

            return tokens;
        }
    }
}
