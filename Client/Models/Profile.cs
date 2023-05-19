namespace Client.Models
{
	public class Profile
	{
		public int Id { get; set; }
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public int Gender { get; set; }
		public DateTime BirthDate { get; set; }
		public string PhoneNumber { get; set; } = null!;
	}
}
