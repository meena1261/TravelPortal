using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class CreditWalletStatement
{
    public int Id { get; set; }

    public int? UsrNo { get; set; }

    public decimal? Amount { get; set; }

    public decimal? Balance { get; set; }

    public string? Factor { get; set; }

    public string? Narration { get; set; }

    public DateTime? AddDate { get; set; }

    public string? Remark { get; set; }

    public string? TransactionId { get; set; }
}
