using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class TblHistoryFundRequest
{
    public int Id { get; set; }

    public int? Usrno { get; set; }

    public decimal? Amount { get; set; }

    public string? Utrno { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string? PaymentMode { get; set; }

    public string? BankName { get; set; }

    public string? TransactionProof { get; set; }

    public DateTime? AddDate { get; set; }

    public bool? IsApproved { get; set; }

    public bool? IsRejected { get; set; }

    public string? Remark { get; set; }

    public DateTime? ActionDate { get; set; }

    public virtual TblMasterUser? UsrnoNavigation { get; set; }
}
