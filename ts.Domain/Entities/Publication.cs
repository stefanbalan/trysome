using System;
using System.Collections.Generic;
using System.Text;

namespace ts.Domain.Entities
{
//    [Table("Publication")]
    public class PolicyPublication : Securable
    {

        public PolicyPublication()
        {
//            SecurableType = Security.SecurableType.PolicyPublication;
//            IsActive = false;
//            Acknowledges = new List<Acknowledge.Acknowledge>();
//            AcknowledgeList = new List<AcknowledgeServer>();
//            Behaviors = new List<PublicationBehavior>();
//            CollectionList = new List<IdCollection>();
        }
        public string Name { get; set; }

        public Guid PublicationID { get; set; }

        public bool IsUtcDateMode { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime? ExpectedDate { get; set; }

        public int PolicyID { get; set; }

        public virtual AbstractPolicy Policy { get; set; }

//        public virtual ICollection<IdCollection> CollectionList { get; set; }
////        [ForeignKey("PublicationID")]
//        public virtual ICollection<PublicationBehavior> Behaviors { get; set; }
////        [JsonIgnore]
//        public virtual ICollection<Acknowledge.Acknowledge> Acknowledges { get; set; }
////        [JsonIgnore]
//        public virtual ICollection<Acknowledge.AcknowledgeImpermanent> ListAcknowledgeImpermanent { get; set; }
////        [JsonIgnore]
//        public virtual ICollection<AcknowledgeServer> AcknowledgeList { get; set; }

        public bool IsActive { get; set; }

        public bool IsImpermanent { get; set; }

        public bool IsArchived { get; set; }
    }
}
