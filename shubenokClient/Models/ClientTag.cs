using System;
using System.Collections.Generic;

namespace shubenokClient.Models;

public partial class ClientTag
{
    public long IdClientTag { get; set; }

    public long IdClient { get; set; }

    public int IdTag { get; set; }

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual Tag IdTagNavigation { get; set; } = null!;
}
