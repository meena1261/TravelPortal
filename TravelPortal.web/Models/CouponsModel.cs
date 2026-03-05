using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelPortal.web.Models
{
    public class AddEditCouponsModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Required")]
        public string CouponCode { get; set; }
        [Required(ErrorMessage = "Required")]
        public string DiscountType { get; set; }
        [Required(ErrorMessage = "Required")]
        public decimal Discount { get; set; }
        [Required(ErrorMessage = "Required")]
        public int MaxUsage { get; set; }
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Date)]
        public DateTime? ExpiryDate { get; set; }
        public bool IsActive { get; set; }
    }
    public class ApplyCouponModel
    {
        public string CouponCode { get; set; }
        public double Amount { get; set; }
    }
}