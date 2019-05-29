namespace ts.Domain.Entities
{
    public class PolicyReboot
    {
        public int Id { get; set; }
        public bool IsEmergency { get; set; }

        public virtual PolicyAbstract IdNavigation { get; set; }
    }
}
