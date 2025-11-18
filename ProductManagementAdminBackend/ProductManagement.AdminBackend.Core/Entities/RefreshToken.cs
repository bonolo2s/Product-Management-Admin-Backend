namespace ProductManagement.AdminBackend.Core.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }

        public Guid AdminId { get; set; }

        public string Token { get; set; } = default!;

        public DateTime ExpiresAt { get; set; }

        public bool IsRevoked { get; set; } = false;
    }
}
