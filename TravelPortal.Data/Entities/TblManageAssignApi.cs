using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class TblManageAssignApi
{
    public int AssignId { get; set; }

    public int Usrno { get; set; }

    public int Apiid { get; set; }

    public bool? IsActive { get; set; }

    public virtual TblManageApi Api { get; set; } = null!;
}
