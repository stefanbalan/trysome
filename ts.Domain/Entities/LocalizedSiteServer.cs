using System;
using System.Collections.Generic;

namespace ts.Domain
{
    public partial class LocalizedSiteServer
    {
        public int SiteId { get; set; }
        public Guid ServerId { get; set; }

        public virtual IdentityServer Server { get; set; }
        public virtual LocalizedSites Site { get; set; }
    }
}
