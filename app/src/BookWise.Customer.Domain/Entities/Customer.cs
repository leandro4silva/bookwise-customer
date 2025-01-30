using Amazon.DynamoDBv2.DataModel;
using BookWise.Customer.Domain.Events;
using BookWise.Customer.Domain.ValueObjects;

namespace BookWise.Customer.Domain.Entities;

public sealed class Customer : AggregateRoot
{
    [DynamoDBProperty]
    public string? Email { get; set; }
    
    [DynamoDBProperty] 
    public string? Password { get; set; }

    [DynamoDBProperty]
    public string? Image { get; set; }

    [DynamoDBProperty]
    public string? FullName { get; set; }

    [DynamoDBProperty]
    public DateTime BirthDate { get; set; }

    [DynamoDBProperty]
    public string? PhoneNumber { get; set; }

    [DynamoDBProperty]
    public CustomerAddress? Address { get; set; }

    public void CreatedAddEvent(CustomerCreated customerCreated) =>
        AddEvent(customerCreated);
}
