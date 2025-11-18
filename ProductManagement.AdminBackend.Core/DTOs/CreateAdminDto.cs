namespace ProductManagement.AdminBackend.Core.DTOs
{
    public class CreateAdminDto
    {
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}

