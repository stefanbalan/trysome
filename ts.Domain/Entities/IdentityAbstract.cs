using System.Collections.Generic;

namespace ts.Domain.Entities
{
    public class IdentityAbstract
    {
        public IdentityAbstract()
        {
            AcknowledgeImpermanents = new HashSet<AcknowledgeImpermanent>();
            Acknowledges = new HashSet<Acknowledge>();
            CollectionResolved = new HashSet<CollectionResolved>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IdentityComputer IdentityComputer { get; set; }
        public virtual IdentityUser IdentityUser { get; set; }
        public virtual ICollection<AcknowledgeImpermanent> AcknowledgeImpermanents { get; set; }
        public virtual ICollection<Acknowledge> Acknowledges { get; set; }
        public virtual ICollection<CollectionResolved> CollectionResolved { get; set; }
    }
}
