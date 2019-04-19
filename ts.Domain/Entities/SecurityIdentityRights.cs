using System;
using System.Collections.Generic;

namespace ts.Domain
{
    public partial class SecurityIdentityRights
    {
        public int Id { get; set; }
        public int SecurityRoleId { get; set; }
        public int AdministrativeIdentityId { get; set; }
        public int SecurityScopeId { get; set; }

        public virtual SecurityIdentity AdministrativeIdentity { get; set; }
        public virtual SecurityRoles SecurityRole { get; set; }
        public virtual SecurityScopes SecurityScope { get; set; }
    }
}
