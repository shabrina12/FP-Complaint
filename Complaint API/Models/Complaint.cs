using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Complaint_API.Models
{
    public partial class Complaint
    {
        public Complaint()
        {
            Resolutions = new HashSet<Resolution>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int OrderId { get; set; }
        public int? Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        [JsonIgnore]
        public virtual Order? Order { get; set; }
        [JsonIgnore]
        public virtual ICollection<Resolution> Resolutions { get; set; }
    }
}
