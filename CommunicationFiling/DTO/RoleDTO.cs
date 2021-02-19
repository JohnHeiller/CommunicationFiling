
namespace CommunicationFiling.DTO
{
    public class RoleDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string RoleName { get; set; }
        public long? AuditId { get; set; }
        public bool IsValid { get; set; }
        public AuditDTO Audit { get; set; }
    }
}
