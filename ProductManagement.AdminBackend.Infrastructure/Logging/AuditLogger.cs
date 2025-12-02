using ProductManagement.AdminBackend.Core.Entities;
using ProductManagement.AdminBackend.Core.Enums;
using ProductManagement.AdminBackend.Core.Interfaces;
using ProductManagement.AdminBackend.Infrastructure.Data;

namespace ProductManagement.AdminBackend.Infrastructure.Logging
{
    public class AuditLogger : IAuditLogger
    {
        private readonly AppDbContext _context;

        public AuditLogger(AppDbContext context)
        {
            _context = context;
        }

        public async Task LogAsync(AuditEvent evt, Guid actorId, Guid targetId)
        {
            var log = new AuditLog
            {
                Id = Guid.NewGuid(),
                Event = evt,
                ActorId = actorId,
                TargetId = targetId,
                Timestamp = DateTime.UtcNow
            };

            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
