using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class TblMasterMenu
{
    public int Id { get; set; }

    public string? MenuName { get; set; }

    public string? Url { get; set; }

    public string? PageName { get; set; }

    public string? Icon { get; set; }

    public string? ControllerName { get; set; }

    public string? ActionName { get; set; }

    public int? MenuLevel { get; set; }

    public int? ParentId { get; set; }

    public int? SubParentId { get; set; }

    public int? Position { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? AddDate { get; set; }
}
