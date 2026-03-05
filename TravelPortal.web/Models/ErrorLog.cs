using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelPortal.web.Models
{
    public class ErrorLog
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Url { get; set; }
        public string HttpMethod { get; set; }
        public string UserHostAddress { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}