using System;
using System.Collections.Generic;

namespace shubenokClient.Models;

public partial class ClientFile
{
    public string Filename { get; set; } = null!;

    public int? IdClient { get; set; }

    public int IdClientFile { get; set; }

    public virtual Client? IdClientNavigation { get; set; }
}
