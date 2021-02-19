using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CommunicationFiling.DAL.Entities
{
    [Table("Audit", Schema = "GEN")]
    public class Audit
    {
        [Key]
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        [ForeignKey("CreationUserRole")]
        public long CreationUserRoleId { get; set; }
        public DateTime? ModificationDate { get; set; }
        [ForeignKey("ModificationUserRole")]
        public long? ModificationUserRoleId { get; set; }
        public bool IsValid { get; set; }
        public UserRole CreationUserRole { get; set; }
        public UserRole ModificationUserRole { get; set; }
    }
}
