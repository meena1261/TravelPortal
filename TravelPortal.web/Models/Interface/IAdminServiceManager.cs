using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelPortal.web.Models.Interface
{
    public interface IAdminServiceManager
    {
        ICouponService CouponService { get; }
        IMenu MenuService { get; }
    }
}