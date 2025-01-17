﻿using Microsoft.AspNetCore.Identity;

namespace EventTracker.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string? Name { get; set; }
		public string? Surname { get; set; }
		public string? Image { get; set; }
	}
}
