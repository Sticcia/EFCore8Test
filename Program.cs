using EFCore8Test;
using Microsoft.EntityFrameworkCore;

var id = Guid.NewGuid();

#region Create Customer

await using (var context = new CustomerContext(new DbContextOptions<CustomerContext>()))
{
	await context.Database.EnsureDeletedAsync();
	await context.Database.EnsureCreatedAsync();

	var repository = new CustomerRepository(context);

	var address = new Address
	{
		Street = "South Street"
	};

	var customer = new Customer
	{
		Id = id,
		Addresses = [address]
	};

	await repository.AddAsync(customer, CancellationToken.None);

	await context.SaveChangesAsync();
}

#endregion


#region Update Address of Customer

await using (var context = new CustomerContext(new DbContextOptions<CustomerContext>()))
{
	var repository = new CustomerRepository(context);

	var customer = await repository.GetByIdAsync(id, CancellationToken.None);

	var newAddress = new Address
	{
		Street = "North Street"
	};

	if (customer != null)
	{
		customer.Addresses = [newAddress];
		repository.Update(customer);

		context.Entry(customer).State = EntityState.Modified;
	}

	await context.SaveChangesAsync();
	// This will throw the following exception:

	// The value of shadow key property 'Address.Id' is unknown when attempting to save changes.
	// This is because shadow property values cannot be preserved when the entity is not being tracked.
	// Consider adding the property to the entity's .NET type.
	// See https://aka.ms/efcore-docs-owned-collections for more information.
}

#endregion
