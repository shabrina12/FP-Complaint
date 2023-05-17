using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Complaint_API.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Resolutions = new HashSet<Resolution>();
        }

        public int Id { get; set; }
        public int ProfileId { get; set; }
        public DateTime HiringDate { get; set; }

        [JsonIgnore]
        public virtual Profile Profile { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Resolution> Resolutions { get; set; }
    }
}
