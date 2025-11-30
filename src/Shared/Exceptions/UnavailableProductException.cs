namespace Shared.Exceptions;

public class UnavailableProductException : Exception
{
    public string? Details { get; }
    
    public UnavailableProductException(string message) : base(message)
    {
    }

    public UnavailableProductException(string message, string details) : base(message)
    {
        Details = details;
    }
}