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

    public class ApiResponse2<T>
    {
        public bool IsSuccess { get; set; }
        public DataContainer<T> Data { get; set; }
        public string Message { get; set; }
        public object Errors { get; set; }
    }

    public class DataContainer<T>
    {
        public string OdataContext { get; set; }
        public List<T> Value { get; set; }
    }

    public class TimelineEvent
    {
        public string Date { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
    }
}