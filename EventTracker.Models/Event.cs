using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventTracker.Models
{
	public class Event
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		public string? Description { get; set; }
		[Required]
		public DateTime DateFrom { get; set; }
		[Required]
		public DateTime DateTo { get; set; }
		public int? Interested { get; set; } = 0;
		public double? TicketPrice { get; set; } = 0;
		[Required]
		public int AgeLimit { get; set; }
		[Required]
		public int CategoryId { get; set; }
		[ForeignKey("CategoryId")]
		[ValidateNever]
		public Category Category { get; set; }
	}
}
