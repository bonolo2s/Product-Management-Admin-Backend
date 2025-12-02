using ProductManagement.AdminBackend.Core.Enums;

namespace ProductManagement.AdminBackend.Core.Interfaces
{
    public interface IAuditLogger
    {
        Task LogAsync(AuditEvent evt, Guid actorId, Guid targetId);
    }
}
