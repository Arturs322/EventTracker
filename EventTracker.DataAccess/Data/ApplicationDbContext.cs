using EventTracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventTracker.DataAccess;
public class ApplicationDbContext : IdentityDbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{
	}
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
	}

	public DbSet<ApplicationUser> ApplicationUsers { get; set; }
	public DbSet<Event> Events { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<Company> Companies { get; set; }
	public DbSet<Country> Countries { get; set; }
}
