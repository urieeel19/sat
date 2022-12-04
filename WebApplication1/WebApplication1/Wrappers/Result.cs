namespace WebApplication1.Wrappers
{
    /// <summary>
    /// Class detailing the result of a specific action.<br></br>
    /// Use it as the return type of a method whenever it could failed, either because of validations or unexpected errors.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Result<T>
    {
        public abstract ResultType ResultType { get; }

        public abstract IList<string> ErrorMessages { get; }

        public abstract T Data { get; }

        public abstract Exception Exception { get; set; }

        public bool IsSuccess() => ResultType == ResultType.Ok;

        public static Result<T> Try<TCaller>(Func<Result<T>> getResult, ILogger<TCaller> logger)
        {
            try
            {
                return getResult();
            }
            catch (Exception e)
            {
                logger.LogError(e, ResultType.UnexpectedError.ToString());
                return new FailedResult<T>(ResultType.UnexpectedError, e);
            }
        }

        /// <summary>
        /// Tries to perform the given <paramref name="getResult"/> action, and returns its <see cref="Result{T}"/>. Otherwise will get a <see cref="FailedResult{T}"/> with the details of the exception.
        /// </summary>
        /// <param name="getResult"></param>
        /// <returns></returns>
        public static async Task<Result<T>> TryAsync<TCaller>(Func<Task<Result<T>>> getResult, ILogger<TCaller> logger)
        {
            try
            {
                return await getResult();
            }
            catch (Exception e)
            {
                logger.LogError(e, ResultType.UnexpectedError.ToString());
                return new FailedResult<T>(ResultType.UnexpectedError, e);
            }
        }

        /// <summary>
        /// Validates whether the current result is OK, and if so, proceeds with the given <paramref name="getResult"/> to process a new result.
        /// </summary>
        /// <typeparam name="TDestination">The type of the following action's result</typeparam>
        /// <param name="getResult">A function that will process the current result's Data and return a new <see cref="Result{T}"/></param>
        /// <returns></returns>
        public async Task<Result<TDestination>> IfOkThenAsync<TDestination>(Func<T, Task<Result<TDestination>>> getResult)
        {
            if (IsSuccess())
            {
                return await getResult(Data);
            }
            return FailedResult<TDestination>.CopyFrom(this);
        }
    }

    public class FailedResult<T> : Result<T>
    {
        private readonly IList<string> _errors;
        private readonly ResultType _resultType;

        public FailedResult(ResultType resultType, IList<string> errors)
        {
            _resultType = resultType;
            _errors = errors;
        }

        public FailedResult(ResultType resultType, string error = null) : this(resultType, new List<string> { error ?? "There was an unexpected problem" })
        {
        }

        public FailedResult(ResultType resultType, Exception exception) : this(resultType, exception.Message)
        {
            Exception = exception;
        }

        public override ResultType ResultType => _resultType;

        public override IList<string> ErrorMessages => _errors;

        public override T Data => default;

        public override Exception Exception { get; set; }

        public static FailedResult<T> CopyFrom<TSource>(Result<TSource> source)
        {
            return new FailedResult<T>(source.ResultType, source.ErrorMessages) { Exception = source.Exception };
        }
    }

    public class SuccessResult<T> : Result<T>
    {
        private readonly T _data;

        public SuccessResult(T data)
        {
            _data = data;
        }

        public override ResultType ResultType => ResultType.Ok;

        public override IList<string> ErrorMessages => null;

        public override T Data => _data;

        public override Exception Exception { get; set; }
    }

    public enum ResultType
    {
        Ok,
        Invalid,
        NotFound,
        UnexpectedError,
        Forbidden
    }
}
