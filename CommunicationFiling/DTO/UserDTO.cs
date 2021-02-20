
namespace CommunicationFiling.DTO
{
    /// <summary>
    /// DTO del registro de usuarios
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Identificador del registro de usuario
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Nombre del usuario
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Apellido del usuario
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Numero de identificacion del usuario
        /// </summary>
        public string NumberID { get; set; }
        /// <summary>
        /// Numero de telefono
        /// </summary>
        public string Phonenumber { get; set; }
        /// <summary>
        /// Direccion de correo electronico
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Direccion de residencia
        /// </summary>
        public string ResidenceAddress { get; set; }
        /// <summary>
        /// Nombre de usuario para el sistema
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Contraseña de usuario
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// ID de registro de auditoria
        /// </summary>
        public long? AuditId { get; set; }
        /// <summary>
        /// Boolean que indica si es valido el registro
        /// </summary>
        public bool IsValid { get; set; }
        public AuditDTO Audit { get; set; }
    }
}
