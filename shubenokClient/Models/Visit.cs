﻿using System;
using System.Collections.Generic;

namespace shubenokClient.Models;

public partial class Visit
{
    public int? IdClient { get; set; }

    public DateTime? TimedateVisit { get; set; }

    public int IdClientVisit { get; set; }

    public virtual Client? IdClientNavigation { get; set; }
}
