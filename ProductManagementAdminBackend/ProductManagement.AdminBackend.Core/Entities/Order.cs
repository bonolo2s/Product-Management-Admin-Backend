using ProductManagement.AdminBackend.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.AdminBackend.Core.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        public string CustomerEmail { get; set; } = default!;

        public List<OrderItem> Items { get; set; } = new();

        public decimal TotalAmount { get; set; }

        public PaymentStatus paymentStatus { get; set; } = PaymentStatus.Pending;

        public OrderStatus orderStatus { get; set; } = OrderStatus.Created;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }

    public class OrderItem
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string ProductName { get; set; } = default!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
