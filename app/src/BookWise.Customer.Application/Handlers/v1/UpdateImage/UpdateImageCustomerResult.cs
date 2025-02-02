using BookWise.Customer.Application.Models.Responses;

namespace BookWise.Customer.Application.Handlers.v1.UpdateImage;

public sealed class UpdateImageCustomerResult
{
    public Guid Id { get; set; }
    public string? Email { get; set; }

    public string? Image { get; set; }

    public string? FullName { get; set; }

    public DateTime BirthDate { get; set; }

    public string? PhoneNumber { get; set; }

    public CustomerAddressResponse? Address { get; set; }
}
