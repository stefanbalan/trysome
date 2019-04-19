using System;
using System.Collections.Generic;

namespace ts.Domain
{
    public partial class AcknowledgeImpermanents
    {
        public int Id { get; set; }
        public int State { get; set; }
        public string StateDescription { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid PublicationId { get; set; }
        public int? IdentityId { get; set; }
        public int? MixedIdentityId { get; set; }
        public int? FeedbackId { get; set; }
        public int? PolicyId { get; set; }
        public int? PublicationId1 { get; set; }

        public virtual AcknowledgeFeedback Feedback { get; set; }
        public virtual IdentityAbstract Identity { get; set; }
        public virtual IdentityUserComputerAssign MixedIdentity { get; set; }
        public virtual PolicyAbstract Policy { get; set; }
        public virtual Publication PublicationId1Navigation { get; set; }
    }
}
