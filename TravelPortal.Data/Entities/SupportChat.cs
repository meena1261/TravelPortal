using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class SupportChat
{
    public int SupportChatId { get; set; }

    public int? SenderUsrno { get; set; }

    public int? ReceiverUsrno { get; set; }

    public string? Message { get; set; }

    public string? Attachfile { get; set; }

    public bool? IsRead { get; set; }

    public bool? IsDelete { get; set; }

    public bool? IsBlock { get; set; }

    public DateTime? AddDate { get; set; }
}
