namespace KickTime.Application.Exceptions;

public sealed class ValidationException : Exception
{
    public IEnumerable<string> Errors { get; }

    public ValidationException(string message,IEnumerable<string>? errors = null): base(message)
    {
        Errors = errors ?? Enumerable.Empty<string>();
    }
}