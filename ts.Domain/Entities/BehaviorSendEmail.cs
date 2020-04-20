namespace ts.Domain.Entities
{
    public class BehaviorSendEmail
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string MailBody { get; set; }

        public virtual BehaviorPublication IdNavigation { get; set; }
    }
}
