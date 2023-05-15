using System;
using System.Collections.Generic;

namespace Complaint_API.Models
{
    public partial class Feedback
    {
        public int Id { get; set; }
        public int ResolutionId { get; set; }
        public string? Description { get; set; }
        public int Rating { get; set; }

        public virtual Resolution Resolution { get; set; } = null!;
    }
}
