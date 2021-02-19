using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CommunicationFiling.DAL.Entities
{
    [Table("User", Schema = "SEG")]
    public class User
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(200)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(50)]
        public string NumberID { get; set; }
        [MaxLength(30)]
        public string Phonenumber { get; set; }
        [MaxLength(150)]
        public string Email { get; set; }
        [MaxLength(250)]
        public string ResidenceAddress { get; set; }
        [MaxLength(50)]
        public string UserName { get; set; }
        [MaxLength(30)]
        public string Password { get; set; }
        [ForeignKey("Audit")]
        public long? AuditId { get; set; }
        public bool IsValid { get; set; }
        public Audit Audit { get; set; }
    }
}
