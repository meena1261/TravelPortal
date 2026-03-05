using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class SupportchatAttachFile
{
    public int AttachFileId { get; set; }

    public int? SupportChatId { get; set; }

    public string? Path { get; set; }

    public string? FileName { get; set; }

    public DateTime? AddDate { get; set; }
}
