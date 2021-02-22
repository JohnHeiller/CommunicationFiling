
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace CommunicationFiling.WebAppClient.DTO
{
    /// <summary>
    /// DTO para tabla de radicados
    /// </summary>
    public class FilingDTO
    {
        /// <summary>
        /// Identificador de radicado
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }
        /// <summary>
        /// Consecutivo del radicado
        /// </summary>
        [JsonProperty("consecutive")]
        public string Consecutive { get; set; }
        /// <summary>
        /// Direccion del documento en servidor o gestor documental
        /// </summary>
        [JsonProperty("storageAddress")]
        public string StorageAddress { get; set; }
        /// <summary>
        /// Data del documento en base64
        /// </summary>
        [JsonProperty("base64File")]
        public string Base64File { get; set; }
        /// <summary>
        /// Documento digital cargado en IFormFile
        /// </summary>
        [JsonProperty("pDFFile")]
        public IFormFile PDFFile { get; set; }
        /// <summary>
        /// ID de usuario del remitente
        /// </summary>
        [JsonProperty("senderUserId")]
        public long SenderUserId { get; set; }
        /// <summary>
        /// ID de usuario del destinatario
        /// </summary>
        [JsonProperty("addresseeUserId")]
        public long AddresseeUserId { get; set; }
        /// <summary>
        /// ID del tipo de correspondencia
        /// </summary>
        [JsonProperty("correspondenceTypeId")]
        public long CorrespondenceTypeId { get; set; }
        /// <summary>
        /// ID del registro de auditoria
        /// </summary>
        [JsonProperty("auditId")]
        public long AuditId { get; set; }
        /// <summary>
        /// Boolean que indica si es valido el registro
        /// </summary>
        [JsonProperty("isValid")]
        public bool IsValid { get; set; }
        [JsonProperty("senderUser")]
        public UserDTO SenderUser { get; set; }
        [JsonProperty("addresseeUser")]
        public UserDTO AddresseeUser { get; set; }
        [JsonProperty("correspondenceType")]
        public CorrespondenceTypeDTO CorrespondenceType { get; set; }
        [JsonProperty("audit")]
        public AuditDTO Audit { get; set; }

    }
}
