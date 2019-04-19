﻿using System;
using System.Collections.Generic;

namespace ts.Domain
{
    public partial class PolicyReboot
    {
        public int Id { get; set; }
        public bool IsEmergency { get; set; }

        public virtual PolicyAbstract IdNavigation { get; set; }
    }
}
