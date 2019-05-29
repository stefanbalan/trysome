using System.Collections.Generic;

namespace ts.Domain.Entities
{
    public class SecurityScope
    {
        public SecurityScope()
        {
            SecurableObject = new HashSet<SecurableObject>();
            SecurityIdentityRights = new HashSet<SecurityIdentityRight>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SecurableObject> SecurableObject { get; set; }
        public virtual ICollection<SecurityIdentityRight> SecurityIdentityRights { get; set; }
    }
}
