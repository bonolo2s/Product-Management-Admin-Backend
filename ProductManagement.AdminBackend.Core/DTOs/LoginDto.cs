namespace ProductManagement.AdminBackend.Core.DTOs
{
    public class LoginDto
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
