using System;
using System.Collections.Generic;

namespace ts.Domain
{
    public partial class IdentityUserComputerAssign
    {
        public IdentityUserComputerAssign()
        {
            AcknowledgeImpermanents = new HashSet<AcknowledgeImpermanents>();
            Acknowledges = new HashSet<Acknowledges>();
            CollectionMixedResolved = new HashSet<CollectionMixedResolved>();
            ComputerAssignationServers = new HashSet<ComputerAssignationServers>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int ComputerId { get; set; }
        public int NbConnection { get; set; }
        public DateTime? LastConnectionDate { get; set; }
        public DateTime? RegistrationDate { get; set; }

        public virtual IdentityComputer Computer { get; set; }
        public virtual IdentityUser User { get; set; }
        public virtual ICollection<AcknowledgeImpermanents> AcknowledgeImpermanents { get; set; }
        public virtual ICollection<Acknowledges> Acknowledges { get; set; }
        public virtual ICollection<CollectionMixedResolved> CollectionMixedResolved { get; set; }
        public virtual ICollection<ComputerAssignationServers> ComputerAssignationServers { get; set; }
    }
}
