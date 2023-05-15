using System;
using System.Collections.Generic;

namespace Complaint_API.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int ProfileId { get; set; }

        public virtual Profile Profile { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
