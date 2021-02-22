using Newtonsoft.Json;
using System;

namespace CommunicationFiling.WebAppClient.DTO
{
    /// <summary>
    /// DTO del registro de auditoria
    /// </summary>
    public class AuditDTO
    {
        /// <summary>
        /// Identificador del registro de auditoria
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        [JsonProperty("creationDate")]
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// ID del usuario que registra la auditoria
        /// </summary>
        [JsonProperty("creationUserId")]
        public long CreationUserId { get; set; }
        /// <summary>
        /// Fecha de modificacion del registro
        /// </summary>
        [JsonProperty("modificationDate")]
        public DateTime? ModificationDate { get; set; }
        /// <summary>
        /// ID del usuario que modifica el registro
        /// </summary>
        [JsonProperty("modificationUserId")]
        public long? ModificationUserId { get; set; }
        /// <summary>
        /// Boolean que indica si es valido el registro
        /// </summary>
        [JsonProperty("isValid")]
        public bool IsValid { get; set; }
        [JsonProperty("creationUser")]
        public UserDTO CreationUser { get; set; }
        [JsonProperty("modificationUser")]
        public UserDTO ModificationUser { get; set; }
    }
}
