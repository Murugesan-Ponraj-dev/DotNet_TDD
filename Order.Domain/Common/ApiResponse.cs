using System.Net;

namespace Order.Domain.Common
{
    public class ApiResponse<T> where T : new()
    {
        public HttpStatusCode StatusCode { get; set; }

        public bool IsSuccess { get; set; }


        public string? Message { get; set; }
        public dynamic? Result { get; set; }

        public static ApiResponse<T> Fail(string? message)
        {
            return new ApiResponse<T> { Message = message, IsSuccess = false };
        }
        public static ApiResponse<T> Fail(Exception e)
        {
            return new ApiResponse<T> { Message = e?.Message, IsSuccess = false };
        }

        public static ApiResponse<T> Success(T value, string? message)
        {
            return new ApiResponse<T> { Result = value, Message = message, IsSuccess = true };
        }

        /// <summary>
        /// Set Success message 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ApiResponse<T> Success(IEnumerable<T> value, string message)
        {
            return new ApiResponse<T> { Result = value, Message = message };
        }

    }
}
