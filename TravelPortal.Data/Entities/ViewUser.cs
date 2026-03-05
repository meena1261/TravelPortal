using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class ViewUser
{
    public string RoleId { get; set; } = null!;

    public string? UserType { get; set; }

    public int Id { get; set; }

    public int? Usrno { get; set; }

    public string? AspNetId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Mobile { get; set; }

    public string? UserId { get; set; }

    public string? Password { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Pincode { get; set; }

    public string? Country { get; set; }

    public int? PackageId { get; set; }

    public int? IntroUsrno { get; set; }

    public string? IntroStr { get; set; }

    public bool IsActive { get; set; }

    public DateTime? Doa { get; set; }

    public bool IsDelete { get; set; }

    public bool IsBlock { get; set; }

    public DateTime AddDate { get; set; }

    public string? Photo { get; set; }

    public int CreatedByUserId { get; set; }

    public bool? IsSales { get; set; }

    public bool? IsPaymentByCc { get; set; }

    public bool? IsPaymentByAccount { get; set; }

    public bool? IsUseLogo { get; set; }

    public bool? IsKycCompleted { get; set; }

    public int? UserTypeId { get; set; }

    public string? RegisterOtp { get; set; }

    public bool? IsContract { get; set; }

    public bool? IsToken { get; set; }

    public string? LookLimitPeriodType { get; set; }

    public int? FlightLookLimit { get; set; }

    public bool? IsSupplier { get; set; }

    public string? SupplierAgreement { get; set; }

    public string? AgreementRemark { get; set; }

    public string Status { get; set; } = null!;
}
