
namespace Models
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get; private set; }
        public T Data { get; private set; }

        public OperationResult()
        {
        }
        private OperationResult(bool isSuccess, T data, string errorMessage)
        {
            IsSuccess = isSuccess;
            Data = data;
            ErrorMessage = errorMessage;
        }

        public static OperationResult<T> Success(T data) => new OperationResult<T>(true, data, null);

        public static OperationResult<T> Failure(string errorMessage) => new OperationResult<T>(false, default, errorMessage);
    }
}
