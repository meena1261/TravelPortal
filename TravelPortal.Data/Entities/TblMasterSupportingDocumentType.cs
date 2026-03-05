using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class TblMasterSupportingDocumentType
{
    public int Id { get; set; }

    public string? SupportingDocumentType { get; set; }

    public bool? IsActive { get; set; }
}
