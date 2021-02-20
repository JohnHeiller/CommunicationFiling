using System;

namespace CommunicationFiling.DTO
{
    /// <summary>
    /// DTO del registro de auditoria
    /// </summary>
    public class AuditDTO
    {
        /// <summary>
        /// Identificador del registro de auditoria
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// ID del usuario que registra la auditoria
        /// </summary>
        public long CreationUserId { get; set; }
        /// <summary>
        /// Fecha de modificacion del registro
        /// </summary>
        public DateTime? ModificationDate { get; set; }
        /// <summary>
        /// ID del usuario que modifica el registro
        /// </summary>
        public long? ModificationUserId { get; set; }
        /// <summary>
        /// Boolean que indica si es valido el registro
        /// </summary>
        public bool IsValid { get; set; }
        public UserDTO CreationUser { get; set; }
        public UserDTO ModificationUser { get; set; }
    }
}
