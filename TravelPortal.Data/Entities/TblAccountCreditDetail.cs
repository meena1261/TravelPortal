using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelPortal.Data.Entities;

[Table("tblAccount_CreditDetails")]
public partial class TblAccountCreditDetail
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    public int? UsrNo { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Amount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Balance { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? Factor { get; set; }

    public string? Narration { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AddDate { get; set; }

    public string? Remark { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? TransactionId { get; set; }
}
