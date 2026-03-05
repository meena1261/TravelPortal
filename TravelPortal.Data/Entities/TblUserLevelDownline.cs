using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class TblUserLevelDownline
{
    public int Id { get; set; }

    public int? Usrno { get; set; }

    public int? DownlineUsrno { get; set; }

    public int? LevelNo { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? AddDate { get; set; }

    public DateTime? Doa { get; set; }
}
