using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CommunicationFiling.DAL.Entities
{
    [Table("RoleAction", Schema = "SEG")]
    public class RoleAction
    {
        [Key]
        public long Id { get; set; }
        [ForeignKey("Role")]
        public long RoleId { get; set; }
        [ForeignKey("Action")]
        public long ActionId { get; set; }
        [ForeignKey("Audit")]
        public long AuditId { get; set; }
        public bool IsValid { get; set; }
        public Role Role { get; set; }
        public Action Action { get; set; }
        public Audit Audit { get; set; }
    }
}
