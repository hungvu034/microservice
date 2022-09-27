namespace Shared.SeedWork
{
    public class ApiErrorResult<T> : ApiResult<T>
    {
        public ApiErrorResult(T data) : base( false , "Error" , data)
        {
        }

        public ApiErrorResult(string message, T data) : base(false, message, data)
        {
        }
    }
}