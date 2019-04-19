using System;
using System.Collections.Generic;

namespace ts.Domain.Entities
{

    //[Table("BehaviorPolicyAbstract")]
    public abstract class AbstractBehavior
    {
        public int PolicyId { get; set; }
        public int ID { get; set; }

        public string Name { get; set; }

        public virtual void ParseParameters(Dictionary<string, string> Params) { }
    }




    //[Table("BehaviorPolicyNotification")]
    public class NotificationBehavior : AbstractBehavior
    {

        public int DisplayTime { get; set; }

        //Define the style of the popup regarding its dimension
        public IntrusionLevel IntrusionLevel { get; set; }

        public TimeSpan ExecutionDateFromExpected { get; set; }

        public TimeSpan RecurrentReminderTimer { get; set; }

        //override the DisplayTime if true, the user must validate he has read the message
        public bool RequiredValidation { get; set; }


        //Title of notification
        public string Title { get; set; }

        //body of notification
        //public string Body { get; set; }

        public int? MessageID { get; set; }

        //[ForeignKey("MessageID")]
        //public virtual MessageContent Message { get; set; }


        //summary of notification
        public string Summary { get; set; }

        //Define the style of the popup regarding its color or 
        public NotificationCriticity NotificationCriticity { get; set; }

        public override void ParseParameters(Dictionary<string, string> Params)
        {
            base.ParseParameters(Params);

            foreach (KeyValuePair<string, string> item in Params)
            {
                Summary = Summary.Replace($"%{item.Key}%", item.Value);
                //Message.Content = Message.Content.Replace($"%{item.Key}%", item.Value);
            }
        }

    }


    //[Table("BehaviorPolicySchedulabe")]
    public class SchedulableBehavior : AbstractBehavior
    {
        public int MaxDeferCount { get; set; }
    }

    public enum NotificationCriticity
    {
        Informational = 0,
        Warning = 1,
        Critical = 2
    }


    public enum IntrusionLevel
    {
        Light = 0,
        Medium = 1,
        High = 2,
        Flashing = 3
    }
}
