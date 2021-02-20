
namespace CommunicationFiling.DTO
{
    /// <summary>
    /// DTO de registro de tabla relacional entre Roles y Acciones
    /// </summary>
    public class RoleActionDTO
    {
        /// <summary>
        /// Identificador del registro
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// ID del rol relacionado
        /// </summary>
        public long RoleId { get; set; }
        /// <summary>
        /// ID de la accion relacionada
        /// </summary>
        public long ActionId { get; set; }
        /// <summary>
        /// ID del registro de auditoria
        /// </summary>
        public long? AuditId { get; set; }
        /// <summary>
        /// Boolean que indica si es valido el registro
        /// </summary>
        public bool IsValid { get; set; }
        public RoleDTO Role { get; set; }
        public ActionDTO Action { get; set; }
        public AuditDTO Audit { get; set; }
    }
}
