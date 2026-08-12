namespace KickTime.Application.Results
{
    public sealed class Result<T>
    {
        public ResultStatus Status { get; }
        public bool IsSuccess => Status == ResultStatus.Success;

        public T? Value { get; }

        public string Message { get; }

        public IEnumerable<string> Errors { get; }

        private Result(
            ResultStatus status,
            T? value,
            string message,
            IEnumerable<string>? errors)
        {
            Status = status;
            Value = value;
            Message = message;
            Errors = errors ?? Enumerable.Empty<string>();
        }

        public static Result<T> Success(T value, string message = "")
        {
            return new Result<T>(
                ResultStatus.Success,
                value,
                message,
                null);
        }

        public static Result<T> Error( string message)
        {
            return new Result<T>(
                ResultStatus.Error,
                default,
                message,
                null);
        }

        public static Result<T> ValidationFailure(string message,IEnumerable<string>? errors = null)
        {
            return new Result<T>(
                ResultStatus.ValidationFailure,
                default,
                message,
                errors);
        }

        public static Result<T> Unauthorized(string message,
            IEnumerable<string>? errors = null)
        {
            return new Result<T>(
                ResultStatus.Unauthorized,
                default,
                message,
                errors);
        }

        public static Result<T> NotFound(string message)
        {
            return new Result<T>(
                ResultStatus.NotFound,
                default,
                message,
                null);
        }

        public static Result<T> Conflict(string message)
        {
            return new Result<T>(
                ResultStatus.Conflict,
                default,
                message,
                null);
        }
    }
}
