using System;
using System.Collections.Generic;

namespace ts.Domain.Entities
{
    public class Publication
    {
        public Publication()
        {
            AcknowledgeImpermanents = new HashSet<AcknowledgeImpermanent>();
            AcknowledgeServers = new HashSet<AcknowledgeServer>();
            Acknowledges = new HashSet<Acknowledge>();
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
        public virtual ICollection<AcknowledgeImpermanent> AcknowledgeImpermanents { get; set; }
        public virtual ICollection<AcknowledgeServer> AcknowledgeServers { get; set; }
        public virtual ICollection<Acknowledge> Acknowledges { get; set; }
        public virtual ICollection<BehaviorPublication> BehaviorPublication { get; set; }
        public virtual ICollection<PublicationOnCollection> PublicationOnCollection { get; set; }
    }
}
