using System;
using System.Collections.Generic;

namespace Complaint_API.Models
{
    public partial class Order
    {
        public Order()
        {
            Complaints = new HashSet<Complaint>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual User Customer { get; set; } = null!;
        public virtual ICollection<Complaint> Complaints { get; set; }
    }
}
