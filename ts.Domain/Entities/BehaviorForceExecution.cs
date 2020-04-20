namespace ts.Domain.Entities
{
    public class BehaviorForceExecution
    {
        public int Id { get; set; }

        public virtual BehaviorPublication IdNavigation { get; set; }
    }
}
