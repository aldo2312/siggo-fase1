using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Siggo.SIGC.Entity
{
    /// <summary>
    /// Clase base que describe la informacion de maker and checker
    /// ---- Estado pendiente -----
    /// Pendiente Autorizar Creación     = P - I
    /// Pendiente Autorizar Modificación = P - U
    /// Pendiente Autorizar Eliminación  = P - C
    /// ---- Estado Autorizado -----
    /// Modificación Autorizada = A - U
    /// Creación Autorizada     = A - I
    /// Eliminación Autorizada  = A - C
    /// 
    /// </summary>
    public class BEMantenimientoBase
    {
        public string Estado { get; set; }
        public string Accion { get; set; }
        public string EstadoDescripcion { get; set; }
        public string Maker { get; set; }
        public string FechaMaker { get; set; }
        public string HoraMaker { get; set; }
        public string Checker { get; set; }
        public string FechaChecker { get; set; }
        public string HoraChecker { get; set; }
    }
}
