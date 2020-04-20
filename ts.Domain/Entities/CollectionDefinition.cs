using System;
using System.Collections.Generic;

namespace ts.Domain.Entities
{
    public class CollectionDefinition
    {
        public CollectionDefinition()
        {
            CollectionMixedResolved = new HashSet<CollectionMixedResolved>();
            CollectionResolved = new HashSet<CollectionResolved>();
            InverseParentCollection = new HashSet<CollectionDefinition>();
            PublicationOnCollection = new HashSet<PublicationOnCollection>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDynamic { get; set; }
        public int ResolutionRecurrence { get; set; }
        public int DataSource { get; set; }
        public int CollectionType { get; set; }
        public string ConnectionString { get; set; }
        public string Filter { get; set; }
        public string DsuserName { get; set; }
        public string Dspassword { get; set; }
        public string Author { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public DateTime? LastResolutionDate { get; set; }
        public int ResolutionStatus { get; set; }
        public int? ParentCollectionId { get; set; }

        public virtual SecurableObject IdNavigation { get; set; }
        public virtual CollectionDefinition ParentCollection { get; set; }
        public virtual ICollection<CollectionMixedResolved> CollectionMixedResolved { get; set; }
        public virtual ICollection<CollectionResolved> CollectionResolved { get; set; }
        public virtual ICollection<CollectionDefinition> InverseParentCollection { get; set; }
        public virtual ICollection<PublicationOnCollection> PublicationOnCollection { get; set; }
    }
}
