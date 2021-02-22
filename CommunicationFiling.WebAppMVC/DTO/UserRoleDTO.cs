
namespace CommunicationFiling.WebAppClient.DTO
{
    /// <summary>
    /// DTO de tabla relacional entre Usuarios y Roles
    /// </summary>
    public class UserRoleDTO
    {
        /// <summary>
        /// Identificador del registro
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// ID del usuario relacionado
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// ID del rol relacionado
        /// </summary>
        public long RoleId { get; set; }
        /// <summary>
        /// ID del registro de auditoria
        /// </summary>
        public long? AuditId { get; set; }
        /// <summary>
        /// Boolean que indica si es valido el registro
        /// </summary>
        public bool IsValid { get; set; }
        public UserDTO User { get; set; }
        public RoleDTO Role { get; set; }
        public AuditDTO Audit { get; set; }
    }
}
