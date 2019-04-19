using System;
using System.Collections.Generic;

namespace ts.Domain
{
    public partial class SecurityPermissions
    {
        public int Id { get; set; }
        public int SecurityRoleId { get; set; }
        public int SecurableType { get; set; }
        public int Ace { get; set; }

        public virtual SecurityRoles SecurityRole { get; set; }
    }
}
