namespace ProductManagement.AdminBackend.Core.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = default!;

        public bool OptedInForPromotions { get; set; } = true;

        public int TotalOrders { get; set; }

        public decimal TotalSpent { get; set; }

        public DateTime FirstPurchaseDate { get; set; }

        public DateTime LastPurchaseDate { get; set; }
    }
}

