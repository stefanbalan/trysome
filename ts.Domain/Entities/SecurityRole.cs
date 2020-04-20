using System.Collections.Generic;

namespace ts.Domain.Entities
{
    public class SecurityRole
    {
        public SecurityRole()
        {
            SecurityIdentityRights = new HashSet<SecurityIdentityRight>();
            SecurityPermissions = new HashSet<SecurityPermission>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SecurityIdentityRight> SecurityIdentityRights { get; set; }
        public virtual ICollection<SecurityPermission> SecurityPermissions { get; set; }
    }
}
