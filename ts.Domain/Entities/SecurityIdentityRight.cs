namespace ts.Domain.Entities
{
    public class SecurityIdentityRight
    {
        public int Id { get; set; }
        public int SecurityRoleId { get; set; }
        public int AdministrativeIdentityId { get; set; }
        public int SecurityScopeId { get; set; }

        public virtual SecurityIdentity AdministrativeIdentity { get; set; }
        public virtual SecurityRole SecurityRole { get; set; }
        public virtual SecurityScope SecurityScope { get; set; }
    }
}
