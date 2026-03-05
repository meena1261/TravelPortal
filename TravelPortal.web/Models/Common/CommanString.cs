using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelPortal.web.Models.Common
{
    public static class CommanString
    {
        public static string Active { get { return "Active"; } }
        public static string Inactive { get { return "Inactive"; } }
        public static string Pending { get { return "PENDING"; } }
        public static string Approved { get { return "APPROVED"; } }
        public static string Rejected { get { return "REJECTED"; } }

        public static class NotesPriority
        {
            public static string Low { get { return "Low"; } }
            public static string Medium { get { return "Medium"; } }
            public static string High { get { return "High"; } }
        }
        public static class NotesTags
        {
            public static string Pending { get { return "Pending"; } }
            public static string Onhold { get { return "Onhold"; } }
            public static string Inprogress { get { return "Inprogress"; } }
            public static string Done { get { return "Done"; } }
        }
    }
}