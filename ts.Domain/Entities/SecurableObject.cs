﻿using System;
using System.Collections.Generic;

namespace ts.Domain
{
    public partial class SecurableObject
    {
        public int Id { get; set; }
        public int SecurityScopeId { get; set; }
        public int SecurableType { get; set; }

        public virtual SecurityScopes SecurityScope { get; set; }
        public virtual CollectionDefinition CollectionDefinition { get; set; }
        public virtual MessageContent MessageContent { get; set; }
        public virtual PolicyAbstract PolicyAbstract { get; set; }
        public virtual Publication Publication { get; set; }
    }
}
