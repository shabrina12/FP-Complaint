using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Complaint_API.Models
{
    public partial class Order
    {
        public Order()
        {
            Complaints = new HashSet<Complaint>();
        }

        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }

        [JsonIgnore]
        public virtual User? Customer { get; set; }
        [JsonIgnore]
        public virtual ICollection<Complaint> Complaints { get; set; }
    }
}
