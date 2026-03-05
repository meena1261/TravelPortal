using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelPortal.web.Models.Interface
{
    public interface IReports
    {
        dynamic Report(SearchModel model);
    }
}