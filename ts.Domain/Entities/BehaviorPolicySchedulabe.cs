namespace ts.Domain.Entities
{
    public class BehaviorPolicySchedulabe
    {
        public int Id { get; set; }
        public int MaxDeferCount { get; set; }

        public virtual BehaviorPolicyAbstract IdNavigation { get; set; }
    }
}
