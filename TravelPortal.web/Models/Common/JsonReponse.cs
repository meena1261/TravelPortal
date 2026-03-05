using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelPortal.web.Models.Common
{
    public class JsonResponse
    {
        public int status { get; set; }
        public string message { get; set; }
        public dynamic data { get; set; }
    }
    public class JsonResponse<T>
    {
        public int status { get; set; }
        public string message { get; set; }
        public T data { get; set; }
    }
}