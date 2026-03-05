using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class TblManageCompanyAccount
{
    public int Id { get; set; }

    public string? AccountNo { get; set; }

    public string? HolderName { get; set; }

    public string? Ifsccode { get; set; }

    public string? BankName { get; set; }

    public string? AccountType { get; set; }

    public string? Branch { get; set; }

    public string? BranchLocation { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? AddDate { get; set; }
}
