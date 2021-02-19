
namespace CommunicationFiling.DTO
{
    public class UserRoleDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public long? AuditId { get; set; }
        public bool IsValid { get; set; }
        public UserDTO User { get; set; }
        public RoleDTO Role { get; set; }
        public AuditDTO Audit { get; set; }
    }
}
