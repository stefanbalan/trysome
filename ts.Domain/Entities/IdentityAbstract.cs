using System;
using System.Collections.Generic;

namespace ts.Domain
{
    public partial class IdentityAbstract
    {
        public IdentityAbstract()
        {
            AcknowledgeImpermanents = new HashSet<AcknowledgeImpermanents>();
            Acknowledges = new HashSet<Acknowledges>();
            CollectionResolved = new HashSet<CollectionResolved>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IdentityComputer IdentityComputer { get; set; }
        public virtual IdentityUser IdentityUser { get; set; }
        public virtual ICollection<AcknowledgeImpermanents> AcknowledgeImpermanents { get; set; }
        public virtual ICollection<Acknowledges> Acknowledges { get; set; }
        public virtual ICollection<CollectionResolved> CollectionResolved { get; set; }
    }
}
