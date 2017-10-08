using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Siggo.SIGC.DataAccess;
using Siggo.SIGC.Entity;

namespace Siggo.SIGC.BusinessLogic
{
    public class BLDato
    {
        public BEDato ObtenerDato(string IdDato)
        {
            return new DADato().ObtenerDato(IdDato);
        }

        public List<BEDato> Listar(string Id, string Nombre)
        {
            return new DADato().Listar(Id, Nombre);
        }

        public bool MantenerDato(int Opcion, BEDato oDato)
        {
            try
            {
                return Convert.ToBoolean(new DADato().MantenerDatos(Opcion, oDato));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
