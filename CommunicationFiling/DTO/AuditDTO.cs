using System;

namespace CommunicationFiling.DTO
{
    public class AuditDTO
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public long CreationUserRoleId { get; set; }
        public DateTime? ModificationDate { get; set; }
        public long? ModificationUserRoleId { get; set; }
        public bool IsValid { get; set; }
        public UserRoleDTO CreationUserRole { get; set; }
        public UserRoleDTO ModificationUserRole { get; set; }
    }
}
