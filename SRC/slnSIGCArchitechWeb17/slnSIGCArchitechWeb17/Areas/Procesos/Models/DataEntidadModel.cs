using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Siggo.SIGC.Entity;
using slnSIGCArchitechWeb17.Models;

namespace slnSIGCArchitechWeb17.Areas.Procesos.Models
{
    public class DataEntidadModel
    {
        public string IdEmpresa { get; set; }
        public string IdCliente { get; set; }
        public string NroOrden { get; set; }
        public string MesInformado { get; set; }
        public string AnhoInformado { get; set; }

        public IEnumerable<ComunModel> lContratas { get; set; }
        public IEnumerable<ComunModel> lClientes { get; set; }
        public IEnumerable<ComunModel> lMeses { get; set; }
        public IEnumerable<ComunModel> lAnhos { get; set; }
        public IEnumerable<ComunModel> lOrdenesPorClientes { get; set; }
    }
}