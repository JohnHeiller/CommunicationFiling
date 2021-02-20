
namespace CommunicationFiling.DTO
{
    /// <summary>
    /// DTO del registro parametrico de roles
    /// </summary>
    public class RoleDTO
    {
        /// <summary>
        /// Identificador del registro parametrico
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Codigo del registro del rol
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Nombre del rol
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// ID del registro de auditoria
        /// </summary>
        public long? AuditId { get; set; }
        /// <summary>
        /// Boolean que indica si es valido el registro
        /// </summary>
        public bool IsValid { get; set; }
        public AuditDTO Audit { get; set; }
    }
}
