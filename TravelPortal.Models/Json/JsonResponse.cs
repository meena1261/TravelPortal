using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPortal.Models.Json
{
    public class JsonResponse
    {
        public JsonResponse() { }
        public int Status { get; set; }
        public string Message { get; set; }
        public dynamic data {  get; set; }
    }
}
