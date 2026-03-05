using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelPortal.web.Models.Interface;

namespace TravelPortal.web.Models.Services
{
    public class AdminServiceManager: IAdminServiceManager
    {
        public ICouponService CouponService { get; private set; }
        public IMenu MenuService { get; private set; }
        public AdminServiceManager()
        {
            CouponService = new CouponService();
            MenuService = new MenuService();
        }
    }
}