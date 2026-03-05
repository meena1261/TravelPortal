using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelPortal.Data.Entities;

public partial class Currency
{
    public int CurrencyId { get; set; }

    [Key]
    [StringLength(3)]
    [Unicode(false)]
    public string CurrencyCode { get; set; } = null!;

    [StringLength(80)]
    public string CurrencyName { get; set; } = null!;

    [StringLength(8)]
    public string? Symbol { get; set; }
}
