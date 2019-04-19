using System;
using System.Collections.Generic;

namespace ts.Domain
{
    public partial class BehaviorPolicySchedulabe
    {
        public int Id { get; set; }
        public int MaxDeferCount { get; set; }

        public virtual BehaviorPolicyAbstract IdNavigation { get; set; }
    }
}
