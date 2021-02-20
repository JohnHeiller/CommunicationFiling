using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunicationFiling.DAL.Entities
{
    [Table("Filing", Schema = "GEN")]
    public class Filing
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [MaxLength(14)]
        public string Consecutive { get; set; }
        [MaxLength(1000)]
        public string StorageAddress { get; set; }
        public string Base64File { get; set; }
        [ForeignKey("SenderUser")]
        public long SenderUserId { get; set; }
        [ForeignKey("AddresseeUser")]
        public long AddresseeUserId { get; set; }
        [ForeignKey("CorrespondenceType")]
        public long CorrespondenceTypeId { get; set; }
        [ForeignKey("Audit")]
        public long AuditId { get; set; }
        public bool IsValid { get; set; }
        public User SenderUser { get; set; }
        public User AddresseeUser { get; set; }
        public CorrespondenceType CorrespondenceType { get; set; }
        public Audit Audit { get; set; }
    }
}
