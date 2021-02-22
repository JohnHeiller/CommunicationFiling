
using Newtonsoft.Json;

namespace CommunicationFiling.WebAppClient.DTO
{
    /// <summary>
    /// DTO del registro de usuarios
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Identificador del registro de usuario
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }
        /// <summary>
        /// Nombre del usuario
        /// </summary>
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        /// <summary>
        /// Apellido del usuario
        /// </summary>
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        /// <summary>
        /// Numero de identificacion del usuario
        /// </summary>
        [JsonProperty("numberID")]
        public string NumberID { get; set; }
        /// <summary>
        /// Numero de telefono
        /// </summary>
        [JsonProperty("phonenumber")]
        public string Phonenumber { get; set; }
        /// <summary>
        /// Direccion de correo electronico
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }
        /// <summary>
        /// Direccion de residencia
        /// </summary>
        [JsonProperty("residenceAddress")]
        public string ResidenceAddress { get; set; }
        /// <summary>
        /// Nombre de usuario para el sistema
        /// </summary>
        [JsonProperty("userName")]
        public string UserName { get; set; }
        /// <summary>
        /// Contraseña de usuario
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }
        /// <summary>
        /// ID de registro de auditoria
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
