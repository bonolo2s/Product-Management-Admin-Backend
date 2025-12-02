using ProductManagement.AdminBackend.Core.Enums;

namespace ProductManagement.AdminBackend.Core.Entities
{
    public class AuditLog
    {
        public Guid Id { get; set; }
        public AuditEvent Event { get; set; }
        public Guid ActorId { get; set; }
        public Guid TargetId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
