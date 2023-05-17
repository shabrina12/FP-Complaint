using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Complaint_API.Models
{
    public partial class Profile
    {
        public Profile()
        {
            Employees = new HashSet<Employee>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Employee> Employees { get; set; }
        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}
