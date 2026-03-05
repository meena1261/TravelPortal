using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelPortal.Data.Entities;

[Table("tblAccount_Credit")]
public partial class TblAccountCredit
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    public int? UsrNo { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Debit { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Credit { get; set; }

    [Column(TypeName = "decimal(19, 2)")]
    public decimal? Balance { get; set; }

    public bool? IsDelete { get; set; }

    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AddDate { get; set; }
}
