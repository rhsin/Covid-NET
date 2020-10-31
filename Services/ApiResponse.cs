using System;
using System.Collections.Generic;
using System.Linq;

namespace Covid.Services
{
    public interface IApiResponse
    {
        public string Method { get; set; }
        public int Count { get; set; }
        public IEnumerable<object> Data { get; set; }
    }

    public class ApiResponse : IApiResponse
    {
        public string Method { get; set; }
        public int Count { get; set; }
        public IEnumerable<object> Data { get; set; }

        public ApiResponse(string method, IEnumerable<object> data)
        {
            Method = method;
            Count = data.Count();
            Data = data;
        }
    }
}
