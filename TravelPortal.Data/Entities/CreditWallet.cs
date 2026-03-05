using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class CreditWallet
{
    public int Id { get; set; }

    public int? UsrNo { get; set; }

    public decimal? Debit { get; set; }

    public decimal? Credit { get; set; }

    public decimal? Balance { get; set; }

    public bool? IsDelete { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? AddDate { get; set; }
}
