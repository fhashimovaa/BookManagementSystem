namespace Application.Exceptions;

[Serializable]
public class BadRequestException : ApplicationException
{
    public BadRequestException(string message) : base(message)
    {
    }
}