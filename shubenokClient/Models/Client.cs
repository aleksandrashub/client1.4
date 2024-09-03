using System;
using System.Collections.Generic;

namespace shubenokClient.Models;

public partial class Client
{
    public long IdClient { get; set; }

    public string? NameClient { get; set; }

    public string? SurnameCl { get; set; }

    public string? OtchestvoCl { get; set; }

    public int? IdGender { get; set; }

    public string? Phone { get; set; }

    public string? Photo { get; set; }

    public DateOnly? Birthday { get; set; }

    public string? Mail { get; set; }

    public DateOnly? DateReg { get; set; }

    public virtual ICollection<ClientFile> ClientFiles { get; set; } = new List<ClientFile>();

    public virtual ICollection<ClientTag> ClientTags { get; set; } = new List<ClientTag>();

    public virtual Gender? IdGenderNavigation { get; set; }

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
