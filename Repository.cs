using Microsoft.EntityFrameworkCore;

namespace EFCore8Test;

public class CustomerRepository(CustomerContext context)
{
	protected readonly DbSet<Customer> DbSet = context.Set<Customer>();

	public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		var keyValues = new object?[] { id };
		return await DbSet.FindAsync(keyValues, cancellationToken);
	}

	public async Task AddAsync(Customer aggregate, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(aggregate);
		await DbSet.AddAsync(aggregate, cancellationToken);
	}

	public void Update(Customer aggregate)
	{
		ArgumentNullException.ThrowIfNull(aggregate);
		DbSet.Update(aggregate);
	}
}