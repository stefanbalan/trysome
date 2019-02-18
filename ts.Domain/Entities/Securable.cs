using System.Collections.Generic;
// ReSharper disable UnusedMember.Global

namespace ts.Domain.Entities
{
    public class SecurityScope
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Securable> Securables { get; set; }


        //public virtual ICollection<SecurityIdentityRight> SecurityIdentityRights { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }


    //[Table("SecurableObject")]
    public abstract class Securable
    {
        public int ID { get; set; }

        public int SecurityScopeId { get; set; }
        //[JsonIgnore]
        //[ForeignKey("SecurityScopeId")]
        public virtual SecurityScope SecurityScope { get; set; }

        public virtual SecurableType SecurableType { get; set; }


        //public virtual ICollection<SecurableDirectory> Directories { get; set; } = new List<SecurableDirectory>();
    }


    public enum SecurableType
    {
        NotificationPolicy = 1,
        ApplicationPolicy = 2,
        RebootPolicy = 3,
        IdCollection = 4,
        PolicyPublication = 5,
        MessageContent = 6,
        Report = 7,
        SurveyPolicy = 8,
        CancelPolicy = 9,
        Message = 10
    }
}
