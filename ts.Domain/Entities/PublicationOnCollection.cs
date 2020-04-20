namespace ts.Domain.Entities
{
    public class PublicationOnCollection
    {
        public int CollectionId { get; set; }
        public int PublicationId { get; set; }

        public virtual CollectionDefinition Collection { get; set; }
        public virtual Publication Publication { get; set; }
    }
}
