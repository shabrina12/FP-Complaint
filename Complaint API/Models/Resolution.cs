using System;
using System.Collections.Generic;

namespace Complaint_API.Models
{
    public partial class Resolution
    {
        public Resolution()
        {
            Feedbacks = new HashSet<Feedback>();
        }

        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int ComplaintId { get; set; }
        public string Description { get; set; } = null!;
        public int? Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public virtual Complaint Complaint { get; set; } = null!;
        public virtual Employee Employee { get; set; } = null!;
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
