using System;
using System.Collections.Generic;

namespace ts.Domain
{
    public partial class Publication
    {
        public Publication()
        {
            AcknowledgeImpermanents = new HashSet<AcknowledgeImpermanents>();
            AcknowledgeServers = new HashSet<AcknowledgeServers>();
            Acknowledges = new HashSet<Acknowledges>();
            BehaviorPublication = new HashSet<BehaviorPublication>();
            PublicationOnCollection = new HashSet<PublicationOnCollection>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Guid PublicationId { get; set; }
        public bool IsUtcDateMode { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? ExpectedDate { get; set; }
        public int PolicyId { get; set; }
        public bool IsActive { get; set; }
        public bool IsImpermanent { get; set; }

        public virtual SecurableObject IdNavigation { get; set; }
        public virtual PolicyAbstract Policy { get; set; }
        public virtual ICollection<AcknowledgeImpermanents> AcknowledgeImpermanents { get; set; }
        public virtual ICollection<AcknowledgeServers> AcknowledgeServers { get; set; }
        public virtual ICollection<Acknowledges> Acknowledges { get; set; }
        public virtual ICollection<BehaviorPublication> BehaviorPublication { get; set; }
        public virtual ICollection<PublicationOnCollection> PublicationOnCollection { get; set; }
    }
}
