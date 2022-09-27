using System.Text.Json.Serialization;

namespace Shared.SeedWork
{
    public class ApiResult<T> 
    {
        [JsonConstructor]
        public ApiResult(bool isSucceeded , string message = null)
        {
            IsSucceeded = isSucceeded ; 
            Message = message ; 
        }

        public ApiResult(bool isSucceeded, string message, T data)
        {
            IsSucceeded = isSucceeded;
            Message = message;
            Data = data;
        }

        public bool IsSucceeded { get; set; }
       public string Message { get; set; }
       public T Data { get; set; } 
    }
}