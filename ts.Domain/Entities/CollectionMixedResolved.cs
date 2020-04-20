namespace ts.Domain.Entities
{
    public class CollectionMixedResolved
    {
        public int IdCollectionId { get; set; }
        public int ComputerAssignationId { get; set; }

        public virtual IdentityUserComputerAssign ComputerAssignation { get; set; }
        public virtual CollectionDefinition IdCollection { get; set; }
    }
}
