using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia
{
    /// <summary>
    /// Clase que contendra las entidades de Template
    /// </summary>
    public class Template
    {
        /// <summary>
        /// Identificador de la clase 
        /// </summary>
        int id { get; set; }
        /// <summary>
        /// identificador de Area
        /// </summary>
        int IdArea { get; set; }
        /// <summary>
        /// Varible tipo strin que contendra la entidad nombre
        /// </summary>
        string Nombre { get; set; }
        /// <summary>
        /// Varible tipo strin que contendra la entidad contenido
        /// </summary>
        string Conenido { get; set; }
        /// <summary>
        /// Varible tipo fecha que contendra la entidad Fecha
        /// </summary>
        DateTime Fecha { get; set; }

    }
}
