namespace Domain.ValueObject;

public class Address : ValueObject
{
    public string ZipCode { get; private set; } = string.Empty;

    public static Address CreateNew(string zipCode)
    {
        Address address = new Address();
        address.ZipCode = zipCode;
        return address;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ZipCode;
    }
}