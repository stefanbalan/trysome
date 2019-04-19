﻿using System;
using System.Collections.Generic;

namespace ts.Domain
{
    public partial class BehaviorPolicyAbstract
    {
        public int Id { get; set; }
        public int PolicyId { get; set; }
        public string Name { get; set; }

        public virtual PolicyAbstract Policy { get; set; }
        public virtual BehaviorPolicyNotification BehaviorPolicyNotification { get; set; }
        public virtual BehaviorPolicySchedulabe BehaviorPolicySchedulabe { get; set; }
    }
}
