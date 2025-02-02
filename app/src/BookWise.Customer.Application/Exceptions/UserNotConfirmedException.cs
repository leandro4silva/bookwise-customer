using ApplicationException = BookWise.Customer.Application.Exceptions.ApplicationException;

namespace BookWise.Customer.Application.Exceptions;

public class UserNotConfirmedException : ApplicationException
{
    public UserNotConfirmedException(string? message) : base(message)
    {
    }
}