using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class TblManageApi
{
    public int Apiid { get; set; }

    public string? Supplier { get; set; }

    public string? Apitype { get; set; }

    public string? LiveEndPointUrl { get; set; }

    public string? LiveClientId { get; set; }

    public string? LiveClientSecret { get; set; }

    public string? TestEndPointUrl { get; set; }

    public string? TestClientId { get; set; }

    public string? TestClientSecret { get; set; }

    public bool? IsLive { get; set; }

    public bool? IsActive { get; set; }

    public int? Priority { get; set; }

    public DateTime? AddDate { get; set; }

    public virtual ICollection<TblManageAssignApi> TblManageAssignApis { get; set; } = new List<TblManageAssignApi>();
}
