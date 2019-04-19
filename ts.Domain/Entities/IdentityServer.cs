using System;
using System.Collections.Generic;

namespace ts.Domain
{
    public partial class IdentityServer
    {
        public IdentityServer()
        {
            AcknowledgeServers = new HashSet<AcknowledgeServers>();
            ComputerAssignationServers = new HashSet<ComputerAssignationServers>();
            InverseParentServer = new HashSet<IdentityServer>();
            LocalizedSiteServer = new HashSet<LocalizedSiteServer>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int ServerRole { get; set; }
        public DateTime LastConnectionDate { get; set; }
        public string Ip { get; set; }
        public double Ram { get; set; }
        public string Cpu { get; set; }
        public double Disk { get; set; }
        public string Os { get; set; }
        public string DataCentername { get; set; }
        public int XmapCoord { get; set; }
        public int YmapCoord { get; set; }
        public int ServerState { get; set; }
        public Guid? ParentServerId { get; set; }

        public virtual IdentityServer ParentServer { get; set; }
        public virtual ICollection<AcknowledgeServers> AcknowledgeServers { get; set; }
        public virtual ICollection<ComputerAssignationServers> ComputerAssignationServers { get; set; }
        public virtual ICollection<IdentityServer> InverseParentServer { get; set; }
        public virtual ICollection<LocalizedSiteServer> LocalizedSiteServer { get; set; }
    }
}
