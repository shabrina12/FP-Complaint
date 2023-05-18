using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Complaint_API.Models
{
    public partial class Feedback
    {
        public int Id { get; set; }
        public int ResolutionId { get; set; }
        public string? Description { get; set; }
        public int Rating { get; set; }

        [JsonIgnore]
        public virtual Resolution Resolution { get; set; } = null!;
    }
}
