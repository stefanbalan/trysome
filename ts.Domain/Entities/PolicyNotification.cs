using System;
using System.Collections.Generic;

namespace ts.Domain
{
    public partial class PolicyNotification
    {
        public int Id { get; set; }

        public virtual PolicyAbstract IdNavigation { get; set; }
    }
}
