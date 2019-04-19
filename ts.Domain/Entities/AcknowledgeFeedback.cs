using System;
using System.Collections.Generic;

namespace ts.Domain
{
    public partial class AcknowledgeFeedback
    {
        public AcknowledgeFeedback()
        {
            AcknowledgeImpermanents = new HashSet<AcknowledgeImpermanents>();
            Acknowledges = new HashSet<Acknowledges>();
        }

        public int Id { get; set; }
        public int? Grade { get; set; }
        public string Comment { get; set; }

        public virtual ICollection<AcknowledgeImpermanents> AcknowledgeImpermanents { get; set; }
        public virtual ICollection<Acknowledges> Acknowledges { get; set; }
    }
}
