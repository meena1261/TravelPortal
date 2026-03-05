using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class Country
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Iso2 { get; set; }

    public string? Iso3 { get; set; }

    public string? PhoneCode { get; set; }

    public string? Currency { get; set; }

    public string? CurrencySymbol { get; set; }
}
