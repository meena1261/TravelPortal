using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class TblManageMenuRight
{
    public int Id { get; set; }

    public int? Usrno { get; set; }

    public int? MenuId { get; set; }

    public bool? IsAdd { get; set; }

    public bool? IsEdit { get; set; }

    public bool? IsDelete { get; set; }
}
