using System;
using System.Collections.Generic;

namespace ts.Domain
{
    public partial class AcknowledgeServers
    {
        public Guid PublicationId { get; set; }
        public Guid ServerId { get; set; }
        public int AcknowledgeState { get; set; }
        public int? PublicationId1 { get; set; }

        public virtual Publication PublicationId1Navigation { get; set; }
        public virtual IdentityServer Server { get; set; }
    }
}
