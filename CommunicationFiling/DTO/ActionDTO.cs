
namespace CommunicationFiling.DTO
{
    /// <summary>
    /// DTO de registro parametrica de accion
    /// </summary>
    public class ActionDTO
    {
        /// <summary>
        /// Identificador de registro de accion
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Codigo del registro de accion
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Nombre de la accion
        /// </summary>
        public string ActionName { get; set; }
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
