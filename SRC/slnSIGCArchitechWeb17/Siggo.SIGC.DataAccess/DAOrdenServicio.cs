using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Siggo.SIGC.Entity;
using Siggo.SIGC.Util;
using System.Data;

namespace Siggo.SIGC.DataAccess
{
    partial class DAOrdenServicioDataContext
    {
    }

    public class DAOrdenServicio
    {
        public List<BEOrdenServicio> Listar(string IdEmpresa, string IdUsuario, string TipoEmpresaSiggo)
        {
            List<BEOrdenServicio> lOrdenes = new List<BEOrdenServicio>();
            try
            {
                using (DAOrdenServicioDataContext dc = new DAOrdenServicioDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_LISTAR_ORDENES((TipoEmpresaSiggo == "CLIENTE" ? 1 : 0), IdEmpresa, IdUsuario);
                    foreach (var item in lnq_Query)
                    {
                        lOrdenes.Add(new BEOrdenServicio()
                        {
                            IdEmpresa = item.CodigoCliente,
                            NroOrden = item.NroOrden,
                            NombreCliente = item.Cliente,
                            NombreContratista = item.Contratista,
                            NombreSubContratista = item.SubContratista,
                            FechaDesde = Utilitario.GetFecha_DDMMYYYY((DateTime)item.FechaControlInicio),
                            FechaHasta = Utilitario.GetFecha_DDMMYYYY((DateTime)item.FechaControlFin),
                            Estado = MC.get_desc_mk(Convert.ToString(item.Estado), ""),
                            FechaMaker = item.FechaMovimiento.ToString(),
                            Maker = item.Usuario
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lOrdenes;
        }

        public List<BEOrdenServicio> Listar(string NroOrden, string Contratista, string Cliente, DateTime FechaDesde, DateTime FechaHasta, string IdUsuario)
        {
            List<BEOrdenServicio> lOrdenes = new List<BEOrdenServicio>();
            try
            {
                using (DAOrdenServicioDataContext dc = new DAOrdenServicioDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_LISTAR_ORDENES_SERVICIO(NroOrden, Contratista, Cliente, FechaDesde, FechaHasta, IdUsuario);
                    foreach (var item in lnq_Query)
                    {
                        lOrdenes.Add(new BEOrdenServicio()
                        {
                            IdEmpresa = item.CodigoCliente,
                            NroOrden = item.NroOrden,
                            CodigoCliente = item.CodigoCliente,
                            NombreCliente = item.Cliente,
                            CodigoContratista = item.CodigoContratista,
                            NombreContratista = item.Contratista,
                            NombreSubContratista = item.SubContratista,
                            FechaDesde = Utilitario.GetFecha_DDMMYYYY((DateTime)item.FechaControlInicio),
                            FechaHasta = Utilitario.GetFecha_DDMMYYYY((DateTime)item.FechaControlFin),
                            Estado = MC.get_desc_mk(Convert.ToString(item.Estado), ""),
                            FechaMaker = item.FechaMovimiento.ToString(),
                            Maker = item.Usuario
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lOrdenes;
        }

        public List<BEOrdenServicioAreas> ObtenerAreas(string IdEmpresa, string NroOrden)
        {
            List<BEOrdenServicioAreas> lstAreas = new List<BEOrdenServicioAreas>();
            try
            {                
                string strSql = "SELECT * FROM REG_ORDEN_SERVICIO_AREAS WHERE IdEmpresa = '" + IdEmpresa + "' and NroOrden = '" + NroOrden + "' ";
                DataTable dtResultado = SqlDataAccess.EjecutarQuery(strSql);

                for (int i = 0; i < dtResultado.Rows.Count; i++)
                {
                    lstAreas.Add(new BEOrdenServicioAreas()
                    {
                        IdEmpresa = dtResultado.Rows[i]["IdEmpresa"].ToString(),
                        NroOrden = dtResultado.Rows[i]["NroOrden"].ToString(),
                        CodigoDpto = dtResultado.Rows[i]["CodigoDpto"].ToString(),
                        DescripcionDpto = dtResultado.Rows[i]["NombreDpto"].ToString()
                    });
                }
                return lstAreas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BEOrdenServicioLocalizaciones> ObtenerSedes(string IdEmpresa, string NroOrden)
        {
            List<BEOrdenServicioLocalizaciones> lstUbicaciones = new List<BEOrdenServicioLocalizaciones>();
            try
            {
                string strSql = "SELECT * FROM REG_ORDEN_SERVICIO_LOCALIZACION WHERE IdEmpresa = '" + IdEmpresa + "' and NroOrden = '" + NroOrden + "' ";
                DataTable dtResultado = SqlDataAccess.EjecutarQuery(strSql);

                for (int i = 0; i < dtResultado.Rows.Count; i++)
                {
                    lstUbicaciones.Add(new BEOrdenServicioLocalizaciones()
                    {
                        IdEmpresa = dtResultado.Rows[i]["IdEmpresa"].ToString(),
                        NroOrden = dtResultado.Rows[i]["NroOrden"].ToString(),
                        CodigoSede = dtResultado.Rows[i]["CodigoSede"].ToString(),
                        DescripcionSede = dtResultado.Rows[i]["DescripcionSede"].ToString()
                    });
                }
                return lstUbicaciones;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BEOrdenServicioContratas> ObtenerContratistas(string IdEmpresa, string NroOrden)
        {
            List<BEOrdenServicioContratas> lContratasOS = new List<BEOrdenServicioContratas>();
            try
            {
                using (DAOrdenServicioDataContext dc = new DAOrdenServicioDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_OBTENER_DATOS_OS_CONTRATA(IdEmpresa, NroOrden);
                    foreach (var item in lnq_Query)
                    {
                        lContratasOS.Add(new BEOrdenServicioContratas()
                        {
                            IdEmpresa = item.IdEmpresa,
                            NroOrden = item.NroOrden,
                            IdContrata = item.IdContrata,
                            DescContrata = item.Contrata,
                            IdSubContrata = item.IdSubContrata,
                            DescSubContrata = item.SubContrata
                        });
                    }
                }
                return lContratasOS;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BEMaestro> ObtenerAreas()
        {
            List<BEMaestro> lTiposAreas = new List<BEMaestro>();
            try
            {
                using (DAMaestroDataContext dc = new DAMaestroDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_OBTENER_LISTA_MAESTRO("60");
                    foreach (var item in lnq_Query)
                    {
                        lTiposAreas.Add(new BEMaestro()
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
            return lTiposAreas;
        }

        public List<BEMaestro> ObtenerSedes()
        {
            List<BEMaestro> lSedes = new List<BEMaestro>();
            try
            {
                using (DAMaestroDataContext dc = new DAMaestroDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_OBTENER_LISTA_MAESTRO("70");
                    foreach (var item in lnq_Query)
                    {
                        lSedes.Add(new BEMaestro()
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
            return lSedes;
        }

        public List<BEEmpresa> ObtenerContratas(string Id, string Usuario)
        {
            List<BEEmpresa> lEmpresas = new List<BEEmpresa>();
            try
            {
                using (DAOrdenServicioDataContext dc = new DAOrdenServicioDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_LISTAR_CONTRATAS(Id, Usuario);
                    foreach (var item in lnq_Query)
                    {
                        lEmpresas.Add(new BEEmpresa()
                        {
                            IdEmpresa = item.IDEMPRESA,
                            RazonSocial = item.RAZONSOCIAL
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lEmpresas;
        }

        public BEOrdenServicio ObtenerOS(string IdEmpresa, string NroOrden)
        {
            BEOrdenServicio retorno = null;
            try
            {
                using (DAOrdenServicioDataContext dc = new DAOrdenServicioDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_OBTENER_DATOS_ORDEN_SERVICIO(IdEmpresa, NroOrden).ToArray();
                    if (lnq_Query.Count() > 0)
                    {
                        retorno = new BEOrdenServicio();
                        var item = lnq_Query.First();
                        retorno.IdEmpresa = item.IdEmpresa;
                        retorno.NroOrden = item.NroOrden;
                        retorno.Descripcion = item.Descripcion;
                        retorno.FechaRealInicio = item.FechaRealInicio;
                        retorno.FechaRealFin = item.FechaRealFin;
                        retorno.FechaControlInicio = item.FechaControlInicio;
                        retorno.FechaControlFin = item.FechaControlFin;
                        retorno.FechaProceso = item.FechaProceso;
                        retorno.HoraProceso = item.HoraProceso;
                        retorno.ProcesadoPor = item.ProcesadoPor;
                        retorno.FechaCierre = item.FechaCierre;
                        retorno.HoraCierre = item.HoraCierre;
                        retorno.CerradoPor = item.CerradoPor;
                        retorno.Estado = Convert.ToString(item.Estado);
                        retorno.EstadoDescripcion = MC.get_desc_mk(Convert.ToString(item.Estado), "");
                        retorno.FechaMaker = item.FechaRegistro.ToString();
                        retorno.HoraMaker = item.HoraRegistro;
                        retorno.Maker = item.RegistradoPor; 
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public int MantenerOrdenServicio(int Opcion, BEOrdenServicio oOrdenServicio, out string strNroOrdenRetorno)
        {
            try
            {
                strNroOrdenRetorno = "";
                using (DAOrdenServicioDataContext dc = new DAOrdenServicioDataContext(Globales.ConfigServidor()))
                {
                    dc.CommandTimeout = 120;

                    bool? bPaso = null;
                    var Lqn_Resultado = dc.SP_MANT_REG_ORDEN_SERVICIO(
                        Opcion, 
                        oOrdenServicio.IdEmpresa, 
                        oOrdenServicio.NroOrden,
                        oOrdenServicio.Descripcion, 
                        oOrdenServicio.FechaRealInicio, 
                        oOrdenServicio.FechaRealFin,
                        oOrdenServicio.FechaControlInicio, 
                        oOrdenServicio.FechaControlFin, 
                        oOrdenServicio.Maker, ref bPaso, ref strNroOrdenRetorno);
                    return Lqn_Resultado == 0 ? 1 : 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MantenerOrdenServicioAreas(int Opcion, List<BEOrdenServicioAreas> lAreas)
        {
            try
            {
                string strSql = "DELETE FROM REG_ORDEN_SERVICIO_AREAS WHERE IdEmpresa = '" + lAreas[0].IdEmpresa + "' and NroOrden = '" + lAreas[0].NroOrden + "' ";
                SqlDataAccess.EjecutarComando(strSql);

                using (DAOrdenServicioDataContext dc = new DAOrdenServicioDataContext(Globales.ConfigServidor()))
                {
                    dc.CommandTimeout = 120;
                    bool? bPaso = null;

                    foreach (var oArea in lAreas)
                    {
                        var Lqn_Resultado = dc.SP_MANT_REG_ORDEN_SERVICIO_AREAS(Opcion,
                        oArea.IdEmpresa, oArea.NroOrden, oArea.CodigoDpto, oArea.DescripcionDpto, ref bPaso);

                        Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                        "DAOrdenServicio.MantenerOrdenServicioAreas", (bPaso == true ? "REGISTRO OK" : "OCURRIO UN ERROR"), NivelMensajeLog.NINGUNO);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MantenerOrdenServicioLocalizaciones(int Opcion, List<BEOrdenServicioLocalizaciones> lLocalizaciones)
        {
            try
            {
                string strSql = "DELETE FROM REG_ORDEN_SERVICIO_LOCALIZACION WHERE IdEmpresa = '" + lLocalizaciones[0].IdEmpresa + "' and NroOrden = '" + lLocalizaciones[0].NroOrden + "' ";
                SqlDataAccess.EjecutarComando(strSql);

                using (DAOrdenServicioDataContext dc = new DAOrdenServicioDataContext(Globales.ConfigServidor()))
                {
                    dc.CommandTimeout = 120;
                    bool? bPaso = null;

                    foreach (var oUbicacion in lLocalizaciones)
                    {
                        var Lqn_Resultado = dc.SP_MANT_REG_ORDEN_SERVICIO_LOCALIZACION(Opcion,
                        oUbicacion.IdEmpresa, oUbicacion.NroOrden, oUbicacion.CodigoSede, oUbicacion.DescripcionSede, ref bPaso);

                        Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                        "DAOrdenServicio.MantenerOrdenServicioLocalizaciones", (bPaso == true ? "REGISTRO OK" : "OCURRIO UN ERROR"), NivelMensajeLog.NINGUNO);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MantenerOrdenServicioContratas(int Opcion, List<BEOrdenServicioContratas> lContratas)
        {
            try
            {
                string strSql = "DELETE FROM REG_ORDEN_SERVICIO_CONTRATA WHERE IdEmpresa = '" + lContratas[0].IdEmpresa + "' and NroOrden = '" + lContratas[0].NroOrden + "' ";
                SqlDataAccess.EjecutarComando(strSql);

                using (DAOrdenServicioDataContext dc = new DAOrdenServicioDataContext(Globales.ConfigServidor()))
                {
                    dc.CommandTimeout = 120;
                    bool? bPaso = null;

                    foreach (var oEmpresa in lContratas)
                    {
                        var Lqn_Resultado = dc.SP_MANT_REG_ORDEN_SERVICIO_CONTRATAS(Opcion,
                        oEmpresa.IdEmpresa, oEmpresa.NroOrden, oEmpresa.IdContrata, oEmpresa.IdSubContrata, ref bPaso);

                        Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                        "DAOrdenServicio.MantenerOrdenServicioContratas", (bPaso == true ? "REGISTRO OK" : "OCURRIO UN ERROR"), NivelMensajeLog.NINGUNO);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BEOrdenServicioPeriodos> ListarPeriodos(string NroOrden, string Contratista, string Cliente, string Mes, string Anho)
        {
            List<BEOrdenServicioPeriodos> lPeriodos = new List<BEOrdenServicioPeriodos>();
            try
            {
                using (DAOrdenServicioDataContext dc = new DAOrdenServicioDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_LISTAR_PERIODOS_GENERADOS(NroOrden, Contratista, Cliente, Mes, Anho);
                    foreach (var item in lnq_Query)
                    {
                        lPeriodos.Add(new BEOrdenServicioPeriodos()
                        {
                            Cliente = item.Cliente,
                            NroOrden = item.NroOrden,
                            Contratista = item.Contratista,
                            Mes = item.Mes,
                            NombreMes = item.NombreMes,
                            Anho = item.Año,
                            Estado = Convert.ToString(item.Estado),
                            EsContratista = item.EsContratista,
                            ValorEmpleado = Convert.ToString(item.Empleados),
                            ValorVehiculo = Convert.ToString(item.Vehiculos),
                            Publicado = Convert.ToString(item.Web),
                            Situacion = item.Situacion
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lPeriodos;
        }

        public void ProcesarPeriodoDeclaracion(string NroOrden, string Contratista, string Cliente, string Mes, string Anho, bool bProcesar)
        {
            try
            {
                string strSql = "UPDATE REG_CONTROL SET FlagPublicado= " + (bProcesar ? 1 : 0);
                strSql += " WHERE IdEmpresa = '" + Contratista + "' and IdCliente='" + Cliente + "' and NroOrden = '" + NroOrden + "' and MesInformado='" + Mes + "' and AnhoInformado='" + Anho + "' ";
                SqlDataAccess.EjecutarComando(strSql);

                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                         "DAOrdenServicio.ProcesarPeriodo", "Ejecutado", NivelMensajeLog.NINGUNO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GenerarPeriodoDeclaracion(string IdEmpresa, string NroOrden, string Mes, string Anho, string RegistradoPor)
        {
            try
            {
                using (DAOrdenServicioDataContext dc = new DAOrdenServicioDataContext(Globales.ConfigServidor()))
                {
                    dc.CommandTimeout = 180;

                    bool? bPaso = null;
                    var Lqn_Resultado = dc.SP_GEN_DOCUMENTACION_MENSUAL(IdEmpresa, NroOrden, Mes, Anho, RegistradoPor, ref bPaso);
                    return Lqn_Resultado == 0 ? 1 : 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ExistePeriodoDeclaracion(string IdEmpresa, string NroOrden, string Mes, string Anho)
        {
            try
            {
                var Lqn_Resultado = -1;
                string
                strSql = "select    count(1) as Cantidad from REG_CONTROL ";
                strSql += "where    IdEmpresa='" + IdEmpresa + "' and NroOrden='" + NroOrden + "' and MesInformado='" + Mes + "' and AnhoInformado='" + Anho + "' ";

                DataTable dtResultado = SqlDataAccess.EjecutarQuery(strSql);

                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                           "DAOrdenServicio.ExistePeriodoDeclaracion", "Ejecutado", NivelMensajeLog.NINGUNO);
                Lqn_Resultado = dtResultado.Rows.Count > 0 ? Convert.ToInt32(dtResultado.Rows[0][0].ToString()) : 0;
                return Lqn_Resultado >= 1 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int QuitarPeriodoDeclaracion(List<BEOrdenServicioPeriodos> lstRequisitos)
        {
            var Lqn_Resultado = -1;
            try
            {
                using (DAOrdenServicioDataContext dc = new DAOrdenServicioDataContext(Globales.ConfigServidor()))
                {
                    dc.CommandTimeout = 120;
                    bool? bPaso = null;

                    foreach (var item in lstRequisitos)
                    {
                        Lqn_Resultado = dc.SP_DEL_DOCUMENTACION_MENSUAL(
                        item.Contratista,
                        item.NroOrden,
                        item.Cliente,
                        item.Mes,
                        item.Anho, ref bPaso);

                        Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                           "DAOrdenServicio.QuitarPeriodoDeclaracion", "Ejecutado", NivelMensajeLog.NINGUNO);
                        Lqn_Resultado.ToString();
                        if (Lqn_Resultado != 0) break;
                    }
                    
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
