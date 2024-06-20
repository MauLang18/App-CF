using System.Collections.Generic;

namespace App_CF.Model
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<T> Data { get; set; }
    }

    public class ApiResponseToken
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
    }
}