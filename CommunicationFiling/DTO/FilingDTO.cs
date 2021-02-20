
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
        /// Documento digital cargado en IFormFile
        /// </summary>
        [NotMapped]
        public IFormFile PDFFile { get; set; }
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

        /// <summary>
        /// Inicializa el DTO validando los datos del documento digitalizado, facilitando el registro en BD
        /// </summary>
        /// <param name="id">ID del radicado</param>
        /// <param name="consecutive">Consecutivo del radicado</param>
        /// <param name="storageAddress">Direccion del archivo en un servidor o gestor documental</param>
        /// <param name="base64File">Data del documento en base 64</param>
        /// <param name="filePDF">documento cargado como IFormFile</param>
        /// <param name="senderUserId">ID de usuario del remitente</param>
        /// <param name="addresseeUserId">ID de usuario del destinatario</param>
        /// <param name="correspondenceTypeId">ID del tipo de correspondencia</param>
        /// <param name="auditId">ID de registro de auditoria</param>
        /// <param name="isValid">bool que indica si el registro es valido o no</param>
        public FilingDTO(long id, string consecutive, string storageAddress, string base64File, IFormFile filePDF, long senderUserId, long addresseeUserId, long correspondenceTypeId, long auditId, bool isValid)
        {
            Id = id;
            Consecutive = consecutive;
            SenderUserId = senderUserId;
            AddresseeUserId = addresseeUserId;
            CorrespondenceTypeId = correspondenceTypeId;
            AuditId = auditId;
            IsValid = isValid;

            if (filePDF != null && string.IsNullOrEmpty(base64File) && string.IsNullOrEmpty(storageAddress))
            {
                using var ms = new MemoryStream();
                filePDF.CopyTo(ms);
                var fileBytes = ms.ToArray();
                string b64Converted = Convert.ToBase64String(fileBytes);
                if (!string.IsNullOrWhiteSpace(b64Converted))
                {
                    Base64File = b64Converted;
                    StorageAddress = string.Empty;
                    PDFFile = null;
                }
            }
            else if (!string.IsNullOrEmpty(base64File) || !string.IsNullOrEmpty(storageAddress))
            {
                Base64File = base64File;
                StorageAddress = storageAddress;
                PDFFile = null;
            }
            else
            {
                Base64File = string.Empty;
                StorageAddress = string.Empty;
                PDFFile = null;
            }
        }
    }
}
