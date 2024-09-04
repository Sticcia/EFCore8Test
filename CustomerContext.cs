using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFCore8Test;

public class CustomerContext(DbContextOptions<CustomerContext> options) : DbContext(options)
{
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder
			.UseSqlServer("Server=127.0.0.1,1433;Database=EFCore8Test;User Id=sa;Password=Password123!;TrustServerCertificate=true;")
			.LogTo(Console.WriteLine, LogLevel.Information);

		// Disabling tracking causes the exception to be thrown
		optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
	}
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Customer>().OwnsMany(
		order => order.Addresses, ownedNavigationBuilder =>
		{
				ownedNavigationBuilder.ToJson();
			});
	}
}
