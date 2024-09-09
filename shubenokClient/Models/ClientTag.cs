using System;
using System.Collections.Generic;

namespace shubenokClient.Models;

public partial class ClientTag
{
    public long IdClient { get; set; }

    public int IdTag { get; set; }

    public virtual Tag IdTagNavigation { get; set; } = null!;
}
