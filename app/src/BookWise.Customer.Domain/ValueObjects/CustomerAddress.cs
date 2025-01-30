using System.Text.Json;

namespace BookWise.Customer.Domain.ValueObjects;

public sealed class CustomerAddress : IEquatable<CustomerAddress>
{
    public string? Street { get; set; }

    public string? Number { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? ZipCode { get; set; }

    public bool Equals(CustomerAddress? other)
    {
        return Street!.Equals(other?.Street, StringComparison.OrdinalIgnoreCase) &&
            Number!.Equals(other?.Number, StringComparison.OrdinalIgnoreCase) &&
            City!.Equals(other?.City, StringComparison.OrdinalIgnoreCase) &&
            State!.Equals(other?.State, StringComparison.OrdinalIgnoreCase) &&
            ZipCode!.Equals(other.ZipCode, StringComparison.OrdinalIgnoreCase);
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Street, Number, City, State, ZipCode);
    }
}
