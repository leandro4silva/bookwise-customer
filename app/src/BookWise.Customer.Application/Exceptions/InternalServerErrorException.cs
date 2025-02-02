namespace BookWise.Customer.Application.Exceptions;

public class InternalServerErrorException : ApplicationException
{
    public InternalServerErrorException(string? message) : base(message)
    {
    }
}