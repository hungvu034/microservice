namespace Shared.SeedWork
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult(T data) : base( true, "Success" , data)
        {

        }

        public ApiSuccessResult(string message, T data) : base(true, message, data)
        {
        }
    }
}