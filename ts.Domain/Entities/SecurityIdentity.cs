using System;
using System.Collections.Generic;

namespace ts.Domain
{
    public partial class SecurityIdentity
    {
        public SecurityIdentity()
        {
            SecurityIdentityRights = new HashSet<SecurityIdentityRights>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int IdentityType { get; set; }

        public virtual ICollection<SecurityIdentityRights> SecurityIdentityRights { get; set; }
    }
}
