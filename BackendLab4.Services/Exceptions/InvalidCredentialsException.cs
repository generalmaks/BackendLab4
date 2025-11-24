namespace BackendLab3.Services.Exceptions;

public class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException() : base() {}
    public InvalidCredentialsException(string message) : base(message) {}
}