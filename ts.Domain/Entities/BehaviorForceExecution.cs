using System;
using System.Collections.Generic;

namespace ts.Domain
{
    public partial class BehaviorForceExecution
    {
        public int Id { get; set; }

        public virtual BehaviorPublication IdNavigation { get; set; }
    }
}
