using System;
using System.Collections.Generic;

namespace ts.Domain
{
    public partial class PolicyAbstract
    {
        public PolicyAbstract()
        {
            AcknowledgeImpermanents = new HashSet<AcknowledgeImpermanents>();
            BehaviorPolicyAbstract = new HashSet<BehaviorPolicyAbstract>();
            Publication = new HashSet<Publication>();
        }

        public int Id { get; set; }
        public string Version { get; set; }
        public int Revision { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string SupportEmail { get; set; }
        public string PrerequisiteCommand { get; set; }
        public string Sender { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual SecurableObject IdNavigation { get; set; }
        public virtual PolicyApplication PolicyApplication { get; set; }
        public virtual PolicyNotification PolicyNotification { get; set; }
        public virtual PolicyReboot PolicyReboot { get; set; }
        public virtual ICollection<AcknowledgeImpermanents> AcknowledgeImpermanents { get; set; }
        public virtual ICollection<BehaviorPolicyAbstract> BehaviorPolicyAbstract { get; set; }
        public virtual ICollection<Publication> Publication { get; set; }
    }
}
