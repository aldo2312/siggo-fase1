using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Siggo.SIGC.Entity;
using Siggo.SIGC.Util;

namespace Siggo.SIGC.DataAccess
{
    partial class DARequisitoDataContext
    {
    }

    public class DARequisito
    {
        public List<BERequisito> Listar(string IdEmpresa, string Id, string Descripcion, string TipoRecurso, string Usuario)
        {
            List<BERequisito> lRequisito = new List<BERequisito>();
            try
            {
                using (DARequisitoDataContext dc = new DARequisitoDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_LISTAR_REQUISITOS(IdEmpresa, Id, Descripcion, TipoRecurso, Usuario);
                    foreach (var item in lnq_Query)
                    {
                        lRequisito.Add(new BERequisito()
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdTipoServicio = Convert.ToString(item.IdTipoServicio),
                            IdTipoDocumento = Convert.ToString(item.IdTipoDocumento),
                            DescripcionRequisito = item.DescripcionRequisito,
                            TipoRiesgo = item.TipoRiesgo,
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
            return lRequisito;
        }

        public BERequisito ObtenerRequisito(string IdEmpresa, string Id, string IdTipoServicio)
        {
            BERequisito retorno = null;
            try
            {
                int Identificador = 0;
                Int32.TryParse(Id, out Identificador);
                using (DARequisitoDataContext dc = new DARequisitoDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_OBTENER_DATOS_REQUISITO(IdEmpresa, Convert.ToInt32(Identificador), Convert.ToInt32(IdTipoServicio)).ToArray();
                    if (lnq_Query.Count() > 0)
                    {
                        retorno = new BERequisito();
                        var item = lnq_Query.First();
                        retorno.IdEmpresa = item.IdEmpresa;
                        retorno.IdTipoServicio = item.IdTipoServicio.ToString();
                        retorno.IdTipoDocumento = Convert.ToString(item.IdTipoDocumento);
                        retorno.DescripcionRequisito = item.DescripcionRequisito;
                        retorno.TipoRecurso = item.TipoRecurso;
                        retorno.DiasTolerancia = item.DiasTolerancia;
                        retorno.DiasNotificacion = item.DiasNotificacion;
                        retorno.Periodicidad = item.Periodicidad;
                        retorno.HabilitaAcceso = item.HabilitaAcceso;                        
                        retorno.HabilitaPago = item.HabilitaPago;
                        retorno.DardeBaja = item.DardeBaja;
                        retorno.FechaVigenciaDesde = item.FechaVigenciaDesde;
                        retorno.FechaVigenciaHasta = item.FechaVigenciaHasta;
                        retorno.Estado = Convert.ToString(item.Estado);
                        retorno.EstadoDescripcion = MC.get_desc_mk(Convert.ToString(item.Estado), Convert.ToString(item.Accion));
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

        public int MantenerRequisito(int Opcion, BERequisito oRequisito)
        {
            try
            {
                using (DARequisitoDataContext dc = new DARequisitoDataContext(Globales.ConfigServidor()))
                {
                    dc.CommandTimeout = 120;

                    bool? bPaso = null;
                    int Identificador = 0;
                    Int32.TryParse(oRequisito.IdTipoDocumento, out Identificador);

                    var Lqn_Resultado = dc.SP_MANT_REG_REQUISITO(Opcion,
                        oRequisito.IdEmpresa, Convert.ToInt32(oRequisito.IdTipoServicio),
                        Identificador,
                        oRequisito.TipoRecurso,
                        oRequisito.DescripcionRequisito,
                        oRequisito.DiasTolerancia,
                        oRequisito.DiasNotificacion,
                        oRequisito.Periodicidad,
                        oRequisito.HabilitaPago,
                        oRequisito.HabilitaAcceso,
                        oRequisito.DardeBaja,
                        oRequisito.FechaVigenciaDesde,
                        oRequisito.FechaVigenciaHasta,
                        oRequisito.Maker, ref bPaso);
                    return Lqn_Resultado == 0 ? 1 : 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BEMaestro> ObtenerPeriodicidad()
        {
            List<BEMaestro> lPeriodicidad = new List<BEMaestro>();
            try
            {
                using (DAMaestroDataContext dc = new DAMaestroDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_OBTENER_LISTA_MAESTRO("50");
                    foreach (var item in lnq_Query)
                    {
                        lPeriodicidad.Add(new BEMaestro()
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
            return lPeriodicidad;
        }

        public List<BERequisitoDato> ObtenerRequisitosDatos(string IdEmpresa, int IdtipoServicio, int Idrequisito)
        {
            List<BERequisitoDato> lRequisitosDatos = new List<BERequisitoDato>();
            try
            {
                using (DARequisitoDataContext dc = new DARequisitoDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_OBTENER_DATOS_REQUISITO_DATO(IdEmpresa, IdtipoServicio, Idrequisito);
                    foreach (var item in lnq_Query)
                    {
                        lRequisitosDatos.Add(new BERequisitoDato()
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdTipoServicio = item.IdTipoServicio.ToString(),
                            IdTipoDocumento = item.IdTipoDocumento.ToString(),
                            IdDato = item.IdDato,
                            DescripcionDato = item.DescripcionDato,
                            Orden = item.Orden
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lRequisitosDatos;
        }

        public void MantenerRequisitoDato(int Opcion, List<BERequisitoDato> lRequisitoDato)
        {
            try
            {
                using (DARequisitoDataContext dc = new DARequisitoDataContext(Globales.ConfigServidor()))
                {
                    dc.CommandTimeout = 120;
                    bool? bPaso = null;

                    foreach (var oDato in lRequisitoDato)
                    {
                        var Lqn_Resultado = dc.SP_MANT_REG_REQUISITO_DATO(Opcion,
                        oDato.IdEmpresa,
                        Convert.ToInt32(oDato.IdTipoServicio),
                        Convert.ToInt32(oDato.IdTipoDocumento),
                        oDato.IdDato,
                        oDato.Orden, oDato.Maker, ref bPaso);

                        Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                        "DARequisito.MantenerRequisitoDato", (bPaso == true ? "REGISTRO OK" : "OCURRIO UN ERROR"), NivelMensajeLog.NINGUNO);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
