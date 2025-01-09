using System.ComponentModel.DataAnnotations;

namespace EventTracker.Models
{
	public class Company
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string? Description { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		public string PhoneNumber { get; set; }
		[Required]
		public string Url { get; set; }
	}
}
