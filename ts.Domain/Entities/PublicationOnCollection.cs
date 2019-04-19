using System;
using System.Collections.Generic;

namespace ts.Domain
{
    public partial class PublicationOnCollection
    {
        public int CollectionId { get; set; }
        public int PublicationId { get; set; }

        public virtual CollectionDefinition Collection { get; set; }
        public virtual Publication Publication { get; set; }
    }
}
