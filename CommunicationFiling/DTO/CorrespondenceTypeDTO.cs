
namespace CommunicationFiling.DTO
{
    /// <summary>
    /// DTO de registro parametrico del tipo de correspondencia
    /// </summary>
    public class CorrespondenceTypeDTO
    {
        /// <summary>
        /// Identificador del registro del tipo de correspondencia
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Codigo del registro
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Nombre del tipo de correspondencia
        /// </summary>
        public string TypeName { get; set; }
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
