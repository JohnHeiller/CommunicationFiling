
namespace CommunicationFiling.WebAppClient.DTO
{
    /// <summary>
    /// DTO para registro y gestion de errores
    /// </summary>
    public class ErrorDTO 
    {
        /// <summary>
        /// Estado del error
        /// </summary>
        public long State { get; set; }
        /// <summary>
        /// Codigo o metodo especifico del error
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Descripcion o mensaje del error
        /// </summary>
        public string Description { get; set; }
    }
}
