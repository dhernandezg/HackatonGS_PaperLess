using System;

namespace Persistencia
{
    /// <summary>
    /// clase que contiene las entidades de ValoresTicket 
    /// </summary>
    public class ValoresTicket
    {
        /// <summary>
        /// entidad tipo string que almacena la clave
        /// </summary>
       public string Clave { get; set; }
        /// <summary>
        /// entidad tipo string que almacena el valor
        /// </summary>
        public string Valor { get; set; }
        /// <summary>
        /// entidad tipo entero que almacena la identificacion del ticket
        /// </summary>
        public int IdTicket { get; set; }
    }
}
