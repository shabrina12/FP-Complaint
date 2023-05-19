using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels
{
	public class RegisterVM
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Gender { get; set; }
		public DateTime BirthDate { get; set; }
		[Phone]
		public string PhoneNumber { get; set; }
		[EmailAddress]
		public string Email { get; set; }
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
