namespace Client.Models
{
	public class Complaint
	{
		public int Id { get; set; }
		public string Title { get; set; } = null!;
		public string Description { get; set; } = null!;
		public int OrderId { get; set; }
		public int? Status { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
	}
}
