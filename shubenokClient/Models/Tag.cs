using System;
using System.Collections.Generic;

namespace shubenokClient.Models;

public partial class Tag
{
    public int IdTag { get; set; }

    public string? NameTag { get; set; }

    public virtual ICollection<ClientTag> ClientTags { get; set; } = new List<ClientTag>();
}
