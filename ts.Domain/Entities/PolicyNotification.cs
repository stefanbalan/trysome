namespace ts.Domain.Entities
{
    public class PolicyNotification
    {
        public int Id { get; set; }

        public virtual PolicyAbstract IdNavigation { get; set; }
    }
}
