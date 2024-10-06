using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TableBookingFrontend.NET.Models
{
	public class CustomerVM
	{
		public int CustomerId { get; set; }
		[Required(ErrorMessage = "First name is required")]
		[StringLength(25, ErrorMessage ="First name cannot exceed 25 characters")]
		[DisplayName("First Name")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "Last name is required")]
		[StringLength(50, ErrorMessage = "LastName cannot exceed 50 characters")]
		[DisplayName("Surname")]
		public string LastName { get; set; }
		[EmailAddress(ErrorMessage ="Invalid email format")]
		public string Email { get; set; }
		[DisplayName("Phone Number")]
		[Phone(ErrorMessage ="Invalid phone number")]
		public string PhoneNumber { get; set; }

		public string FullName => $"{FirstName} {LastName}";
	}
}
