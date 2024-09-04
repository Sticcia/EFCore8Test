namespace EFCore8Test;

public class Customer
{
	public required Guid Id { get; init; }

	public required List<Address> Addresses { get; set; }
}

public class Address
{
	public required string Street { get; set; }
}
