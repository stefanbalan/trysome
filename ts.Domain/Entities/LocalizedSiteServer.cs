using System;

namespace ts.Domain.Entities
{
    public class LocalizedSiteServer
    {
        public int SiteId { get; set; }
        public Guid ServerId { get; set; }

        public virtual IdentityServer Server { get; set; }
        public virtual LocalizedSite Site { get; set; }
    }
}
