﻿using System;

namespace ts.Domain.Entities
{
    public class ComputerAssignationServers
    {
        public int ComputerAssignationId { get; set; }
        public Guid DbServerIdentityId { get; set; }

        public virtual IdentityUserComputerAssign ComputerAssignation { get; set; }
        public virtual IdentityServer DbServerIdentity { get; set; }
    }
}
