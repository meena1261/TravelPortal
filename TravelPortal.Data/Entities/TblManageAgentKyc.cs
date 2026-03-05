using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class TblManageAgentKyc
{
    public int Id { get; set; }

    public int Usrno { get; set; }

    public int? AgencyTypeId { get; set; }

    public string? AgencyName { get; set; }

    public string? PanNumber { get; set; }

    public string? PancardImg { get; set; }

    public string? GstNumber { get; set; }

    public int? BusinessTypeId { get; set; }

    public string? AgencyAddress { get; set; }

    public string? Pincode { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public int? KycDocumentTypeId { get; set; }

    public string? DocumentFile { get; set; }

    public int? SupportingDocumentTypeId { get; set; }

    public string? SupportingDocumentFile { get; set; }

    public bool IsApproved { get; set; }

    public bool IsRejected { get; set; }

    public string? Remark { get; set; }

    public DateTime AddDate { get; set; }

    public int? CreatedByUsrno { get; set; }

    public virtual TblMasterUser UsrnoNavigation { get; set; } = null!;
}
