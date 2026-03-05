using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class TblMasterBank
{
    public int Id { get; set; }

    public string? BankName { get; set; }

    public DateTime? AddDate { get; set; }

    public bool? IsActive { get; set; }
}
