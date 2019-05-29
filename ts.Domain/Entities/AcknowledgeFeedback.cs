using System.Collections.Generic;

namespace ts.Domain.Entities
{
    public class AcknowledgeFeedback
    {
        public AcknowledgeFeedback()
        {
            AcknowledgeImpermanents = new HashSet<AcknowledgeImpermanent>();
            Acknowledges = new HashSet<Acknowledge>();
        }

        public int Id { get; set; }
        public int? Grade { get; set; }
        public string Comment { get; set; }

        public virtual ICollection<AcknowledgeImpermanent> AcknowledgeImpermanents { get; set; }
        public virtual ICollection<Acknowledge> Acknowledges { get; set; }
    }
}
