using System;
using System.Collections.Generic;

namespace shubenokClient.Models;

public partial class Visit
{
    public long IdClientVisit { get; set; }

    public long? IdClient { get; set; }

    public DateTime? TimedateVisit { get; set; }

    public virtual Client? IdClientNavigation { get; set; }
}
