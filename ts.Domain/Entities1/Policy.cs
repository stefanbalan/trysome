using System;
using System.Collections.Generic;

namespace ts.Domain.Entities
{
    public class Site
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Server> Servers { get; set; }
    }

    public class Server
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<Site> Sites { get; set; }
    }

    public class Policy
    {
        public int Id { get; set; }
        public PolicyType Type { get; set; }
        public string Name { get; set; }
    }

    public enum PolicyType
    {
        NotSet = 0,
        Restart,
        Install,
        Alert
    }



//    [Table("PolicyAbstract")]
    public abstract class AbstractPolicy : Securable
    {
        //public int ID { get; set; }

        public AbstractPolicy()
        {
            Behaviors = new List<AbstractBehavior>();
        }


        public string Version { get; set; }

        public int Revision { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public string Category { get; set; }

        public string SupportEmail { get; set; }

        public string PrerequisiteCommand { get; set; }

        public string Sender { get; set; }

        public DateTime CreationDate { get; set; }

//        [ForeignKey("PolicyId")]
        public virtual ICollection<AbstractBehavior> Behaviors { get; set; }

//        public virtual ICollection<PolicyPublication> PublicationList { get; set; }

    }
}