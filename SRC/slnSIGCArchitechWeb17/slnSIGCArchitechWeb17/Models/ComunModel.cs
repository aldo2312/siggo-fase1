using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace slnSIGCArchitechWeb17.Models
{
    public class ComunModel
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }

        public ComunModel()
        {

        }
        public ComunModel(string codigo, string descripcion)
        {
            this.Codigo = codigo;
            this.Descripcion = descripcion;
        }
    }
}