using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class TblManageCreditLimit
{
    public int Id { get; set; }

    public int? Usrno { get; set; }

    public int? CreditTypeId { get; set; }

    public decimal? Amount { get; set; }

    public decimal? RequestedAmount { get; set; }

    public decimal? UsedAmount { get; set; }

    public decimal? AvailableAmount { get; set; }

    public int? Days { get; set; }

    public string? Remark { get; set; }

    public string? AdminRemark { get; set; }

    public string? AttachFile { get; set; }

    public bool? IsApproved { get; set; }

    public bool? IsRejected { get; set; }

    public DateTime? ActionDate { get; set; }

    public int? ActionByUsrno { get; set; }

    public DateTime? AddDate { get; set; }
}
