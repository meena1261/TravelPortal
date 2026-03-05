using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelPortal.web.Models.Common;

namespace TravelPortal.web.Models.Interface
{
    public interface ICouponService
    {
        List<AddEditCouponsModel> GetAll();
        AddEditCouponsModel GetById(int id);

        JsonResponse AddEdit(AddEditCouponsModel model);
        bool Delete(int id);
        bool ToggleActive(int id);
    }
}