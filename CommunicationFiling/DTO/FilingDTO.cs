
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace CommunicationFiling.DTO
{
    /// <summary>
    /// DTO para tabla de radicados
    /// </summary>
    public class FilingDTO
    {
        /// <summary>
        /// Identificador de radicado
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Consecutivo del radicado
        /// </summary>
        public string Consecutive { get; set; }
        /// <summary>
        /// Direccion del documento en servidor o gestor documental
        /// </summary>
        public string StorageAddress { get; set; }
        /// <summary>
        /// Data del documento en base64
        /// </summary>
        public string Base64File { get; set; }
        /// <summary>
        /// ID de usuario del remitente
        /// </summary>
        public long SenderUserId { get; set; }
        /// <summary>
        /// ID de usuario del destinatario
        /// </summary>
        public long AddresseeUserId { get; set; }
        /// <summary>
        /// ID del tipo de correspondencia
        /// </summary>
        public long CorrespondenceTypeId { get; set; }
        /// <summary>
        /// ID del registro de auditoria
        /// </summary>
        public long? AuditId { get; set; }
        /// <summary>
        /// Boolean que indica si es valido el registro
        /// </summary>
        public bool IsValid { get; set; }
        public UserDTO SenderUser { get; set; }
        public UserDTO AddresseeUser { get; set; }
        public CorrespondenceTypeDTO CorrespondenceType { get; set; }
        public AuditDTO Audit { get; set; }
    }
}
