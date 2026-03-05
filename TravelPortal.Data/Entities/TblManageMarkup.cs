using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class TblManageMarkup
{
    public int Id { get; set; }

    public int? MarkupTypeId { get; set; }

    public decimal? Value { get; set; }

    public int? CurrencyId { get; set; }

    public int? MethodId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? MarkupCategoryId { get; set; }

    public int? Usrno { get; set; }

    public int? AirlineId { get; set; }

    public int? CreatedByUsrno { get; set; }

    public bool? IsUniversal { get; set; }

    public bool? IsUniversalOverride { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? AddDate { get; set; }
}
