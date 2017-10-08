using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Siggo.SIGC.Entity;
using Siggo.SIGC.Util;

namespace Siggo.SIGC.DataAccess
{
    public class DATipoServicio
    {

        public BETipoServicio ObtenerTipoServicio(string IdTipoServicio)
        {
            BETipoServicio retorno = null;
            try
            {
                using (DATipoServicioDataContext dc = new DATipoServicioDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_OBTENER_DATOS_TIPO_SERVICIO(IdTipoServicio).ToArray();
                    if (lnq_Query.Count() > 0)
                    {
                        retorno = new BETipoServicio();
                        var item = lnq_Query.First();
                        retorno.IdTipoServicio = Convert.ToString(item.IdTipoServicio);
                        retorno.Descripcion = item.Descripcion;
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

        public List<BETipoServicio> Listar(string Id, string Descripcion)
        {
            List<BETipoServicio> lTipoServicio = new List<BETipoServicio>();
            try
            {
                using (DATipoServicioDataContext dc = new DATipoServicioDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_LISTAR_TIPO_SERVICIO(Id, Descripcion);
                    foreach (var item in lnq_Query)
                    {
                        lTipoServicio.Add(new BETipoServicio()
                        {
                            IdTipoServicio = Convert.ToString(item.IdTipoServicio),
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
            return lTipoServicio;
        }

        public int MantenerTipoServicio(int Opcion, BETipoServicio oTipoServicio)
        {
            try
            {
                using (DATipoServicioDataContext dc = new DATipoServicioDataContext(Globales.ConfigServidor()))
                {
                    bool? bPaso = null;
                    var Lqn_Resultado = dc.SP_MANT_REG_TIPO_SERVICIO(Opcion,
                        Convert.ToInt32(oTipoServicio.IdTipoServicio),
                        oTipoServicio.Descripcion,
                        oTipoServicio.Maker, ref bPaso);
                    return Lqn_Resultado == 0 ? 1 : 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BETipoDocumento> ObtenerTipoDocsServicio(string IdTipoServicio)
        {
            List<BETipoDocumento> lTipoDocs = new List<BETipoDocumento>();
            try
            {
                using (DATipoServicioDataContext dc = new DATipoServicioDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_OBTENER_TIPO_DOCUMENTOS_SERVICIO(Convert.ToInt32(IdTipoServicio));
                    foreach (var item in lnq_Query)
                    {
                        lTipoDocs.Add(new BETipoDocumento()
                        {
                            IdTipoDocumento = Convert.ToString(item.Codigo),
                            Descripcion = item.Descripcion
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lTipoDocs;
        }
    }
}