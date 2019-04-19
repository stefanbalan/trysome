using System;
using System.Collections.Generic;

namespace ts.Domain
{
    public partial class MessageContent
    {
        public MessageContent()
        {
            BehaviorPolicyNotification = new HashSet<BehaviorPolicyNotification>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsTemplate { get; set; }
        public string Content { get; set; }

        public virtual SecurableObject IdNavigation { get; set; }
        public virtual ICollection<BehaviorPolicyNotification> BehaviorPolicyNotification { get; set; }
    }
}
