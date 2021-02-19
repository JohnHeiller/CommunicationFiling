
namespace CommunicationFiling.DTO
{
    public class UserDTO
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NumberID { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
        public string ResidenceAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public long? AuditId { get; set; }
        public bool IsValid { get; set; }
        public AuditDTO Audit { get; set; }
    }
}
