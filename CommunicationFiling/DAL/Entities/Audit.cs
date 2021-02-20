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
        [ForeignKey("CreationUser")]
        public long CreationUserId { get; set; }
        public DateTime? ModificationDate { get; set; }
        [ForeignKey("ModificationUser")]
        public long? ModificationUserId { get; set; }
        public bool IsValid { get; set; }
        public User CreationUser { get; set; }
        public User ModificationUser { get; set; }
    }
}
