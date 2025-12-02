using BCrypt.Net;
using ProductManagement.AdminBackend.Core.Interfaces;

namespace ProductManagement.AdminBackend.Infrastructure.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
            => BCrypt.Net.BCrypt.HashPassword(password);

        public bool Verify(string hash, string password)
            => BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
