
namespace CommunicationFiling.DTO
{
    public class ActionDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string ActionName { get; set; }
        public long? AuditId { get; set; }
        public bool IsValid { get; set; }
        public AuditDTO Audit { get; set; }
    }
}
