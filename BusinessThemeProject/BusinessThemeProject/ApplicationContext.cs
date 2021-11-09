using BusinessThemeProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessThemeProject
{
	public class ApplicationContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public ApplicationContext(DbContextOptions<ApplicationContext> options)
			: base(options)
		{
			//Database.EnsureCreated();
		}
	}
}