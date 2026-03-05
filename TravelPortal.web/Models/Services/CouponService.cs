using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelPortal.EDMX;
using TravelPortal.web.Models.Common;
using TravelPortal.web.Models.Interface;

namespace TravelPortal.web.Models.Services
{
    public class CouponService : ICouponService
    {
        private readonly db_silviEntities _context;
        public CouponService()
        {
            _context = new db_silviEntities();
        }
        public JsonResponse AddEdit(AddEditCouponsModel model)
        {
            JsonResponse response = new JsonResponse();
            try
            {
                var obj = _context.tblManage_Coupons.FirstOrDefault(x => x.ID == model.Id);
            }
            catch (Exception ex)
            {
                response.status = 0;
                response.message = "An error occurred: " + ex.Message;
            }
            return response;
        }
        public List<AddEditCouponsModel> GetAll()
        {
            throw new NotImplementedException();
        }
        public AddEditCouponsModel GetById(int id)
        {
            throw new NotImplementedException();
        }
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        public bool ToggleActive(int id)
        {
            throw new NotImplementedException();
        }

    }
}