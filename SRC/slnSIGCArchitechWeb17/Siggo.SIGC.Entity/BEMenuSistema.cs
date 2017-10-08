using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siggo.SIGC.Entity
{
    public class BEMenuSistema
    {
        public int IdMenu { get; set; }
        public int PreMenu { get; set; }
        public bool Grupo { get; set; }
        public string DescripcionMenu { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
    }

    public class BERoles
    {
        public int IDRol { get; set; }
        public string DescripcionRol { get; set; }
    }
}
