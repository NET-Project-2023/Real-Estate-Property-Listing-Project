namespace Real_estate.Domain.Common
{
    public class Result<T> where T : class
    {
        public Result(bool isSucces, T value, string error)
        {
            IsSucces = isSucces;
            Value = value;
            Error = error;
        }

        public bool IsSucces { get; }
        public T Value { get; }
        public string Error { get; }
        public static Result<T> Succes(T valeu)
        {
            return new Result<T>(true, valeu, null!);
        }
        public static Result<T> Failure(string error)
        {
            return new Result<T>(false, null!, error);
        }
    }
}

