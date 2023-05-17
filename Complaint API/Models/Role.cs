using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Complaint_API.Models
{
    public partial class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
