using System;
using System.Collections.Generic;

namespace ts.Domain
{
    public partial class PolicyApplication
    {
        public int Id { get; set; }
        public string PackageId { get; set; }
        public int ApplicationAction { get; set; }
        public string PreinstallKilledProcessList { get; set; }
        public int PostInstallSystemEvent { get; set; }

        public virtual PolicyAbstract IdNavigation { get; set; }
    }
}
