using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class TblManageCoupon
{
    public int Id { get; set; }

    public string? CouponCode { get; set; }

    public string? DiscountType { get; set; }

    public decimal? Discount { get; set; }

    public int? MaxUsage { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? AddDate { get; set; }

    public string? Description { get; set; }
}
