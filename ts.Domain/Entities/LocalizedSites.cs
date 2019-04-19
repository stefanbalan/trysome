using System;
using System.Collections.Generic;

namespace ts.Domain
{
    public partial class LocalizedSites
    {
        public LocalizedSites()
        {
            LocalizedSiteServer = new HashSet<LocalizedSiteServer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public virtual ICollection<LocalizedSiteServer> LocalizedSiteServer { get; set; }
    }
}
