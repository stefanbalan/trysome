namespace ts.Domain.Entities
{
    public class SecurityPermission
    {
        public int Id { get; set; }
        public int SecurityRoleId { get; set; }
        public int SecurableType { get; set; }
        public int Ace { get; set; }

        public virtual SecurityRole SecurityRole { get; set; }
    }
}
