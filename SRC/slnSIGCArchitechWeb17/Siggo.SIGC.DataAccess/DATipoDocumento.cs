using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Siggo.SIGC.Entity;
using Siggo.SIGC.Util;

namespace Siggo.SIGC.DataAccess
{
    public class DATipoDocumento
    {
        public BETipoDocumento ObtenerTipoDocumento(string IdTipoDocumento, string IdTipoServicio)
        {
            BETipoDocumento retorno = null;
            try
            {
                using (DATipoDocumentoDataContext dc = new DATipoDocumentoDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_OBTENER_DATOS_TIPO_DOCUMENTO(Convert.ToInt32(IdTipoDocumento), Convert.ToInt32(IdTipoServicio)).ToArray();
                    if (lnq_Query.Count() > 0)
                    {
                        retorno = new BETipoDocumento();
                        var item = lnq_Query.First();
                        retorno.IdTipoDocumento = Convert.ToString(item.IdTipoDocumento);
                        retorno.IdTipoServicio = Convert.ToString(item.IdTipoServicio);
                        retorno.Descripcion = item.Descripcion;
                        retorno.DescripcionLarga = item.DescripcionLarga;
                        retorno.IdTipoRiesgo = item.IdTipoRiesgo;
                        retorno.NivelRiesgo = item.NivelRiesgo;
                        retorno.Estado = Convert.ToString(item.Estado);
                        retorno.FechaMaker = item.FECMAKER;
                        retorno.HoraMaker = item.HoraRegistro;
                        retorno.Maker = item.MAKER;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public List<BETipoDocumento> Listar(string Id, string Descripcion, string Servicio)
        {
            List<BETipoDocumento> lTiposDocumento = new List<BETipoDocumento>();
            try
            {
                using (DATipoDocumentoDataContext dc = new DATipoDocumentoDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_LISTAR_TIPOS_DOCUMENTOS(Id, Descripcion, Servicio);
                    foreach (var item in lnq_Query)
                    {
                        lTiposDocumento.Add(new BETipoDocumento()
                        {
                            IdTipoDocumento = Convert.ToString(item.IdTipoDocumento),
                            IdTipoServicio = Convert.ToString(item.IdTipoServicio),
                            Descripcion = item.Descripcion,
                            DescripcionLarga = item.DescripcionLarga,
                            NivelRiesgo = item.DescNivelRiesgo,
                            TipoRiesgo = item.TipoRiesgo,
                            TipoServicio = item.Servicio,
                            Estado = MC.get_desc_mk(Convert.ToString(item.ESTADO), Convert.ToString(item.ACCION)),
                            FechaMaker = item.FECHAREGISTRO.ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lTiposDocumento;
        }

        public int MantenerTipoDocumento(int Opcion, BETipoDocumento oTipoDocumento)
        {
            try
            {
                using (DATipoDocumentoDataContext dc = new DATipoDocumentoDataContext(Globales.ConfigServidor()))
                {
                    bool? bPaso = null;
                    var Lqn_Resultado = dc.SP_MANT_REG_TIPO_DOCUMENTO(Opcion,
                        Convert.ToInt32(oTipoDocumento.IdTipoDocumento), 
                        oTipoDocumento.Descripcion, oTipoDocumento.DescripcionLarga,
                        Convert.ToInt32(oTipoDocumento.IdTipoServicio),
                        Convert.ToInt32(oTipoDocumento.IdTipoRiesgo),
                        oTipoDocumento.IdNivelRiesgo, oTipoDocumento.Maker, ref bPaso);
                    return Lqn_Resultado == 0 ? 1 : 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BEMaestro> ObtenerTiposRiesgo()
        {
            List<BEMaestro> lTiposRiesgo = new List<BEMaestro>();
            try
            {
                using (DAMaestroDataContext dc = new DAMaestroDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_OBTENER_LISTA_MAESTRO("40");
                    foreach (var item in lnq_Query)
                    {
                        lTiposRiesgo.Add(new BEMaestro()
                        {
                            Codigo = item.Codigo,
                            Descripcion = item.Descripcion,
                            Valor = item.Valor
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lTiposRiesgo;
        }

        public List<BEMaestro> ObtenerNivelesRiesgo()
        {
            List<BEMaestro> lNiveles = new List<BEMaestro>();
            try
            {
                using (DAMaestroDataContext dc = new DAMaestroDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_OBTENER_LISTA_MAESTRO("10");
                    foreach (var item in lnq_Query)
                    {
                        lNiveles.Add(new BEMaestro()
                        {
                            Codigo = item.Codigo,
                            Descripcion = item.Descripcion,
                            Valor = item.Valor
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lNiveles;
        }

    }
}
