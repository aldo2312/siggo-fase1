using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Siggo.SIGC.Entity;
using Siggo.SIGC.Util;

namespace Siggo.SIGC.DataAccess
{
    public class DARecurso
    {
        public List<BERecurso> Listar(string IdEmpresa, string Id, string Descripcion, string TipoRecurso)
        {
            List<BERecurso> lRecursos = new List<BERecurso>();
            try
            {
                using (DARecursoDataContext dc = new DARecursoDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_LISTAR_RECURSOS(IdEmpresa, Id, Descripcion, TipoRecurso);
                    foreach (var item in lnq_Query)
                    {
                        lRecursos.Add(new BERecurso()
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdRecurso = item.IdRecurso,
                            NumeroReferencia = item.NroReferencia,
                            Descripcion4 = item.DescripcionRecurso,
                            Observacion = item.DescripcionLarga,
                            TipoRecurso = item.TipoRecurso,
                            DescripcionTipoRecurso = item.DescripcionTipoRecurso,
                            NombreEmpresa = item.Contratista,
                            Estado = MC.get_desc_mk(Convert.ToString(item.ESTADO), Convert.ToString(item.ACCION)),
                            FechaMaker = item.FECHAREGISTRO.ToShortDateString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lRecursos;
        }

        public BERecurso ObtenerRecurso(string IdEmpresa, string Id)
        {
            BERecurso retorno = null;
            try
            {
                using (DARecursoDataContext dc = new DARecursoDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_OBTENER_DATOS_RECURSO(IdEmpresa, Id).ToArray();
                    if (lnq_Query.Count() > 0)
                    {
                        retorno = new BERecurso();
                        var item = lnq_Query.First();
                        retorno.IdEmpresa = item.IdEmpresa;
                        retorno.IdRecurso = item.IdRecurso;
                        retorno.NumeroReferencia = item.NroReferencia;
                        retorno.Descripcion1 = item.Descripcion1;
                        retorno.Descripcion2 = item.Descripcion2;
                        retorno.Descripcion3 = item.Descripcion3;
                        retorno.Descripcion4 = item.Descripcion4;
                        retorno.Observacion = item.Observacion;
                        retorno.TipoRecurso = item.TipoRecurso;
                        retorno.Cantidad = item.Cantidad;
                        retorno.Estado = Convert.ToString(item.Estado);
                        retorno.EstadoDescripcion = MC.get_desc_mk(Convert.ToString(item.Estado), Convert.ToString(item.Accion));
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

        public int MantenerRecurso(int Opcion, BERecurso oRecurso)
        {
            try
            {
                using (DARecursoDataContext dc = new DARecursoDataContext(Globales.ConfigServidor()))
                {
                    dc.CommandTimeout = 120;

                    bool? bPaso = null;

                    var Lqn_Resultado = dc.SP_MANT_REG_RECURSO(Opcion,
                        oRecurso.IdEmpresa,
                        oRecurso.IdRecurso,
                        oRecurso.NumeroReferencia,
                        oRecurso.Descripcion1,
                        oRecurso.Descripcion2,
                        oRecurso.Descripcion3,
                        oRecurso.Descripcion4,
                        oRecurso.Observacion,
                        oRecurso.Cantidad,
                        oRecurso.TipoRecurso,
                        oRecurso.Maker, ref bPaso);
                    return Lqn_Resultado == 0 ? 1 : 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MantenerRequisitoRecurso(int Opcion, List<BERequisitoRecurso> lRequisitoRecurso)
        {
            try
            {
                using (DARecursoDataContext dc = new DARecursoDataContext(Globales.ConfigServidor()))
                {
                    dc.CommandTimeout = 120;
                    bool? bPaso = null;

                    foreach (var oRecurso in lRequisitoRecurso)
                    {                        
                        var Lqn_Resultado = dc.SP_MANT_REG_REQUISITO_RECURSO(Opcion,
                        oRecurso.IdEmpresa,
                        oRecurso.IdRecurso,
                        Convert.ToInt32(oRecurso.IdTipoServicio),
                        Convert.ToInt32(oRecurso.IdTipoDocumento),
                        oRecurso.EsRequerido,
                        oRecurso.NecesitaAdjunto,
                        oRecurso.Orden,
                        oRecurso.Maker, ref bPaso);

                        Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                        "DARecurso.MantenerRequisitoRecurso", (bPaso == true ? "REGISTRO OK" : "OCURRIO UN ERROR"), NivelMensajeLog.NINGUNO);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BEMaestro> ObtenerTiposRecurso()
        {
            List<BEMaestro> lTiposRecurso = new List<BEMaestro>();
            try
            {
                using (DAMaestroDataContext dc = new DAMaestroDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_OBTENER_LISTA_MAESTRO("30");
                    foreach (var item in lnq_Query)
                    {
                        lTiposRecurso.Add(new BEMaestro()
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
            return lTiposRecurso;
        }

        public List<BERequisitoRecurso> ObtenerRequisitosRecurso(string IdEmpresa, string IdRecurso)
        {
            List<BERequisitoRecurso> lRequisitosRecurso = new List<BERequisitoRecurso>();
            try
            {
                using (DARecursoDataContext dc = new DARecursoDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_OBTENER_DATOS_REQUISITO_RECURSO(IdEmpresa, IdRecurso);
                    foreach (var item in lnq_Query)
                    {
                        lRequisitosRecurso.Add(new BERequisitoRecurso()
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdTipoServicio = item.IdTipoServicio.ToString(),
                            IdTipoDocumento = item.IdTipoDocumento.ToString(),
                            DescripcionDocumento = item.DescripcionRequisito,
                            EsRequerido = item.EsRequerido,
                            NecesitaAdjunto = item.NecesitaAdjunto,
                            Orden = item.Orden
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lRequisitosRecurso;
        }

        public List<BETipoServicio> ObtenerServicio()
        {
            List<BETipoServicio> lTiposServicio = new List<BETipoServicio>();
            try
            {
                using (DATipoServicioDataContext dc = new DATipoServicioDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_LISTAR_TIPO_SERVICIO("", "");
                    foreach (var item in lnq_Query)
                    {
                        lTiposServicio.Add(new BETipoServicio()
                        {
                            IdTipoServicio = Convert.ToString(item.IdTipoServicio),
                            Descripcion = item.Descripcion
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lTiposServicio;
        }

        public List<BERequisito> ObtenerRequisitoPorServicio(string IdEmpresa, string IdTipoServicio, string TipoRecurso)
        {
            List<BERequisito> lRequisitos = new List<BERequisito>();
            try
            {
                using (DARecursoDataContext dc = new DARecursoDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_OBTENER_DATOS_REQUISITO_SERVICIO(IdEmpresa, Convert.ToInt32(IdTipoServicio), TipoRecurso);
                    foreach (var item in lnq_Query)
                    {
                        lRequisitos.Add(new BERequisito()
                        {
                            IdTipoDocumento = Convert.ToString(item.IdTipoDocumento),
                            DescripcionRequisito = item.DescripcionRequisito
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lRequisitos;
        }

    }
}
