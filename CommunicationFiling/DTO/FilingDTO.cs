
namespace CommunicationFiling.DTO
{
    public class FilingDTO
    {
        public long Id { get; set; }
        public string Consecutive { get; set; }
        public string StorageAddress { get; set; }
        public string Base64File { get; set; }
        public long SenderUserId { get; set; }
        public long AddresseeUserId { get; set; }
        public long CorrespondenceTypeId { get; set; }
        public long? AuditId { get; set; }
        public bool IsValid { get; set; }
        public UserDTO SenderUser { get; set; }
        public UserDTO AddresseeUser { get; set; }
        public CorrespondenceTypeDTO CorrespondenceType { get; set; }
        public AuditDTO Audit { get; set; }
    }
}
