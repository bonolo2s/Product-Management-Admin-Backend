using ProductManagement.AdminBackend.Core.DTOs;
using ProductManagement.AdminBackend.Core.Entities;

namespace ProductManagement.AdminBackend.Core.Interfaces
{
    public interface ITokenService
    {
        AuthResponseDto GenerateTokens(Admin admin);
    }
}
