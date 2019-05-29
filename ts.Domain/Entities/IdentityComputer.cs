using System.Collections.Generic;

namespace ts.Domain.Entities
{
    public class IdentityComputer
    {
        public IdentityComputer()
        {
            IdentityUserComputerAssign = new HashSet<IdentityUserComputerAssign>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string AodVersion { get; set; }
        public string ComputerDataXml { get; set; }

        public virtual IdentityAbstract IdNavigation { get; set; }
        public virtual ICollection<IdentityUserComputerAssign> IdentityUserComputerAssign { get; set; }
    }
}
