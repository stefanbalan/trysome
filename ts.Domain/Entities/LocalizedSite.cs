using System.Collections.Generic;

namespace ts.Domain.Entities
{
    public class LocalizedSite
    {
        public LocalizedSite()
        {
            LocalizedSiteServers = new HashSet<LocalizedSiteServer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public virtual ICollection<LocalizedSiteServer> LocalizedSiteServers { get; set; }
    }
}
