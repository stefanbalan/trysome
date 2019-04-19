using System;
using System.Collections.Generic;

namespace ts.Domain
{
    public partial class CollectionResolved
    {
        public int CollectionId { get; set; }
        public int IdentityId { get; set; }

        public virtual CollectionDefinition Collection { get; set; }
        public virtual IdentityAbstract Identity { get; set; }
    }
}
