using System.Collections.Generic;

namespace ts.Domain.Entities
{
    public class SecurityIdentity
    {
        public SecurityIdentity()
        {
            SecurityIdentityRights = new HashSet<SecurityIdentityRight>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int IdentityType { get; set; }

        public virtual ICollection<SecurityIdentityRight> SecurityIdentityRights { get; set; }
    }
}
