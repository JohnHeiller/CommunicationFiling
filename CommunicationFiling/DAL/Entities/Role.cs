using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CommunicationFiling.DAL.Entities
{
    [Table("Role", Schema = "ADM")]
    public class Role
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string Code { get; set; }
        [Required]
        [MaxLength(100)]
        public string RoleName { get; set; }
        [ForeignKey("Audit")]
        public long? AuditId { get; set; }
        public bool IsValid { get; set; }
        public Audit Audit { get; set; }
    }
}
