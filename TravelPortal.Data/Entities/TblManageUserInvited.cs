using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class TblManageUserInvited
{
    public int Id { get; set; }

    public int? RoleId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Mobile { get; set; }

    public int? InvitedUsrno { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? AddDate { get; set; }
}
