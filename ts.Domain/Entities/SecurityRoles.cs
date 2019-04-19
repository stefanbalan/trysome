using System;
using System.Collections.Generic;

namespace ts.Domain
{
    public partial class SecurityRoles
    {
        public SecurityRoles()
        {
            SecurityIdentityRights = new HashSet<SecurityIdentityRights>();
            SecurityPermissions = new HashSet<SecurityPermissions>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SecurityIdentityRights> SecurityIdentityRights { get; set; }
        public virtual ICollection<SecurityPermissions> SecurityPermissions { get; set; }
    }
}
