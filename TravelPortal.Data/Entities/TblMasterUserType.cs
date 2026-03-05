using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class TblMasterUserType
{
    public int Id { get; set; }

    public string? UserType { get; set; }

    public int? CreatedByUsrno { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDelete { get; set; }

    public DateTime? AddDate { get; set; }

    public int? ModifyByUsrno { get; set; }

    public DateTime? ModifyDate { get; set; }
}
