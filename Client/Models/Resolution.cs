namespace Client.Models
{
	public class Resolution
	{
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int ComplaintId { get; set; }
        public string Description { get; set; } = null!;
        public int? Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
