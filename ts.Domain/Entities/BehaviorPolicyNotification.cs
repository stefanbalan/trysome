using System;

namespace ts.Domain.Entities
{
    public class BehaviorPolicyNotification
    {
        public int Id { get; set; }
        public int DisplayTime { get; set; }
        public int IntrusionLevel { get; set; }
        public TimeSpan ExecutionDateFromExpected { get; set; }
        public TimeSpan RecurrentReminderTimer { get; set; }
        public bool RequiredValidation { get; set; }
        public string Title { get; set; }
        public int? MessageId { get; set; }
        public string Summary { get; set; }
        public int NotificationCriticity { get; set; }

        public virtual BehaviorPolicyAbstract IdNavigation { get; set; }
        public virtual MessageContent Message { get; set; }
    }
}
