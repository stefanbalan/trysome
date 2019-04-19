using System;
using System.Collections.Generic;
using System.Text;

namespace ts.Domain.Entities
{
//    [Table("CollectionDefinition")]
    public class IdCollection : Securable
    {
        public string Name { get; set; }
        public bool IsDynamic { get; set; }

        public int ResolutionRecurrence { get; set; }

        public CollectionDataSource DataSource { get; set; }
        public CollectionType CollectionType { get; set; }

        public string ConnectionString { get; set; }

        public string Filter { get; set; }

        public string DSUserName { get; set; }
        //public SecureString DSPassword { get; set; }
        public string DSPassword { get; set; }

        public string Author { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public DateTime? LastResolutionDate { get; set; }

        public ResolutionStatus ResolutionStatus { get; set; }


//        public virtual ICollection<CollectionIdentityRel> IdentitiesRel { get; set; } = new List<CollectionIdentityRel>();
//        public IEnumerable<AbstractIdentity> Identities => IdentitiesRel?.Select(rel => rel.Identity);
//
//        public virtual ICollection<CollectionMixedIdentityRel> MixedIdentitiesRel { get; set; } = new List<CollectionMixedIdentityRel>();
//
//        public virtual ICollection<PolicyPublication> PublicationList { get; set; } = new List<PolicyPublication>();


        public int? ParentCollectionID { get; set; }
//        [ForeignKey("ParentCollectionID")]
        public virtual IdCollection ParentCollection { get; set; }
//        [ForeignKey("ParentCollectionID")]
        public virtual ICollection<IdCollection> ChildCollection { get; set; }

        public IdCollection()
        {
            SecurableType = SecurableType.IdCollection;
        }

        public override bool Equals(object obj)
        {
            return obj.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }

    }


    [Flags]
    public enum CollectionType
    {
        User = 1,
        Computer = 2,
    }



    public enum ResolutionStatus
    {
        New = 0,
        Resolving = 1,
        Resolved = 2,
        Failed = 3
    }


    public enum CollectionDataSource
    {
        ActiveDirectory = 0,
        CSVFile = 1,
        XMLFile = 2,
        SQLDb = 3,
        SCCM = 4
    }
}
