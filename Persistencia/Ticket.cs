using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia
{
    /// <summary>
    /// Clase que contendra las entidades de ticket
    /// </summary>
   public  class Ticket
    {
        /// <summary>
        /// Entiad tipo  entero que contiene el identificador del ticket
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Entiad tipo  entero que contiene la el idnetificador de template.
        /// </summary>
        public int IdTemplate { get; set; }
        /// <summary>
        /// Entiad tipo  Date que contiene la fecha del ticket
        /// </summary>
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Entiad tipo  string que contiene la descripcion del ticket
        /// </summary>
        public string Descripcion { get; set; }

    }
}
