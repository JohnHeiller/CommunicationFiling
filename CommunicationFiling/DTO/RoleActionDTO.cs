
namespace CommunicationFiling.DTO
{
    public class RoleActionDTO
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public long ActionId { get; set; }
        public long? AuditId { get; set; }
        public bool IsValid { get; set; }
        public RoleDTO Role { get; set; }
        public ActionDTO Action { get; set; }
    }
}
