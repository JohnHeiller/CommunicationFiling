using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CommunicationFiling.DAL.Entities
{
    [Table("UserRole", Schema = "SEG")]
    public class UserRole
    {
        [Key]
        public long Id { get; set; }
        [ForeignKey("User")]
        public long UserId { get; set; }
        [ForeignKey("Role")]
        public long RoleId { get; set; }
        [ForeignKey("Audit")]
        public long? AuditId { get; set; }
        public bool IsValid { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
        public Audit Audit { get; set; }
    }
}
