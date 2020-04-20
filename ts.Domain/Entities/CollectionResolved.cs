namespace ts.Domain.Entities
{
    public class CollectionResolved
    {
        public int CollectionId { get; set; }
        public int IdentityId { get; set; }

        public virtual CollectionDefinition Collection { get; set; }
        public virtual IdentityAbstract Identity { get; set; }
    }
}
