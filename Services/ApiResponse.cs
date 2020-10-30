using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid.Services
{
    public interface IApiResponse
    {
        public object Json(string method, IEnumerable<object> data);
    }

    public class ApiResponse : IApiResponse
    {
        public object Json(string method, IEnumerable<object> data)
        {
            return new
            {
                Method = method,
                Count = data.Count(),
                Data = data
            };
        }
    }
}
