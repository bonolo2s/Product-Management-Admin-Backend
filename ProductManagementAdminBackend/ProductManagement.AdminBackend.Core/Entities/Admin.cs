using ProductManagement.AdminBackend.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.AdminBackend.Core.Entities
{
    public class Admin
    {
        public Guid Id { get; set; }
        
        public string FullName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string PasswordHash { get; set; } = default!;

        public AdminRole adminRole { get; set; } = AdminRole.SuperAdmin;


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? LastLogin { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
