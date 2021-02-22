
using Newtonsoft.Json;

namespace CommunicationFiling.WebAppClient.DTO
{
    /// <summary>
    /// DTO de registro parametrico del tipo de correspondencia
    /// </summary>
    public class CorrespondenceTypeDTO
    {
        /// <summary>
        /// Identificador del registro del tipo de correspondencia
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }
        /// <summary>
        /// Codigo del registro
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }
        /// <summary>
        /// Nombre del tipo de correspondencia
        /// </summary>
        [JsonProperty("typeName")]
        public string TypeName { get; set; }
        /// <summary>
        /// ID del registro de auditoria
        /// </summary>
        [JsonProperty("auditId")]
        public long? AuditId { get; set; }
        /// <summary>
        /// Boolean que indica si es valido el registro
        /// </summary>
        [JsonProperty("isValid")]
        public bool IsValid { get; set; }
        [JsonProperty("audit")]
        public AuditDTO Audit { get; set; }
    }
}
