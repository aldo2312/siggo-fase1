using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Siggo.SIGC.Entity;
using Siggo.SIGC.Util;

namespace Siggo.SIGC.DataAccess
{
    partial class DADatoDataContext
    {
    }

    public class DADato

    {
        public BEDato ObtenerDato(string IdDato)
        {
            BEDato retorno = null;
            try
            {
                using (DADatoDataContext dc = new DADatoDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_OBTENER_DATOS_DATO(IdDato).ToArray();
                    if (lnq_Query.Count() > 0)
                    {
                        retorno = new BEDato();
                        var item = lnq_Query.First();
                        retorno.IdDato = item.IdDato;
                        retorno.Descripcion = item.Descripcion;
                        retorno.Alias = item.Alias;
                        retorno.Categoria = item.CategoriaDato;
                        retorno.Estado = Convert.ToString(item.Estado);
                        retorno.FechaMaker = item.FECMAKER;
                        retorno.HoraMaker = item.HoraRegistro;
                        retorno.Maker = item.MAKER; //corregir
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public List<BEDato> Listar(string Id, string Descripcion)
        {
            List<BEDato> lDatos = new List<BEDato>();
            try
            {
                using (DADatoDataContext dc = new DADatoDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_LISTAR_DATOS(Id, Descripcion);
                    foreach (var item in lnq_Query)
                    {
                        lDatos.Add(new BEDato()
                        {
                            IdDato = item.IdDato,
                            Descripcion = item.Descripcion,
                            Estado = MC.get_desc_mk(Convert.ToString(item.Estado), Convert.ToString(item.Accion)),
                            FechaMaker = item.FechaRegistro.ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lDatos;
        }

        public int MantenerDatos(int Opcion, BEDato oDato)
        {
            try
            {
                using (DADatoDataContext dc = new DADatoDataContext(Globales.ConfigServidor()))
                {
                    bool? bPaso = null;
                    var Lqn_Resultado = dc.SP_MANT_REG_DATOS(Opcion,
                        oDato.IdDato,
                        oDato.Descripcion, oDato.Alias, oDato.Categoria,
                        oDato.Maker, ref bPaso);
                    return Lqn_Resultado == 0 ? 1 : 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}