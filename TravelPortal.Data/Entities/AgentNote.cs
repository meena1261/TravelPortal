using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class AgentNote
{
    public int NotesId { get; set; }

    public int? Usrno { get; set; }

    public string? NotesTitle { get; set; }

    public string? Tag { get; set; }

    public string? Priority { get; set; }

    public string? Descriptions { get; set; }

    public bool? IsImportant { get; set; }

    public bool? IsDelete { get; set; }

    public DateTime? AddDate { get; set; }
}
