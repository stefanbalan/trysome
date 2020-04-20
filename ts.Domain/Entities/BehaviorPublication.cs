namespace ts.Domain.Entities
{
    public class BehaviorPublication
    {
        public int Id { get; set; }
        public int PublicationId { get; set; }
        public string Name { get; set; }

        public virtual Publication Publication { get; set; }
        public virtual BehaviorForceExecution BehaviorForceExecution { get; set; }
        public virtual BehaviorSendEmail BehaviorSendEmail { get; set; }
    }
}
