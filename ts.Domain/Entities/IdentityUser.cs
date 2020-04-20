using System.Collections.Generic;

namespace ts.Domain.Entities
{
    public class IdentityUser
    {
        public IdentityUser()
        {
            IdentityUserComputerAssign = new HashSet<IdentityUserComputerAssign>();
        }

        public int Id { get; set; }
        public string DomainName { get; set; }

        public virtual IdentityAbstract IdNavigation { get; set; }
        public virtual ICollection<IdentityUserComputerAssign> IdentityUserComputerAssign { get; set; }
    }
}
