using System;
using System.Collections.Generic;

namespace ts.Domain
{
    public partial class SecurityScopes
    {
        public SecurityScopes()
        {
            SecurableObject = new HashSet<SecurableObject>();
            SecurityIdentityRights = new HashSet<SecurityIdentityRights>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SecurableObject> SecurableObject { get; set; }
        public virtual ICollection<SecurityIdentityRights> SecurityIdentityRights { get; set; }
    }
}
