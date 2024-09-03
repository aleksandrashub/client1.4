using System;
using System.Collections.Generic;

namespace shubenokClient.Models;

public partial class ClientFile
{
    public long IdClientFile { get; set; }

    public long IdClient { get; set; }

    public string Filename { get; set; } = null!;

    public virtual Client IdClientNavigation { get; set; } = null!;
}
