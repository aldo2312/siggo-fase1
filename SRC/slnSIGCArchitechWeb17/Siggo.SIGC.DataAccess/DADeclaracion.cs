using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Siggo.SIGC.Entity;
using Siggo.SIGC.Util;
using System.Data;
using System.Data.SqlClient;

namespace Siggo.SIGC.DataAccess
{
    partial class DADeclaracionDataContext
    {
    }

    public class DADeclaracion
    {
        public List<vReg_Declaracion> Listar(string IdEmpresa, string IdCliente, string NroOrden, string TipoRecurso, string EstadoRegistro, string MesControl, string AnhoControl, string IdUsuario, string IdRegistro)
        {
            List<vReg_Declaracion> lstDeclaraciones = new List<vReg_Declaracion>();
            try
            {
                using (DADeclaracionDataContext dc = new DADeclaracionDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_LISTAR_DECLARACIONES(NroOrden, IdEmpresa, IdCliente, MesControl, AnhoControl, TipoRecurso, IdUsuario, IdRegistro, 1, 500);
                    foreach (var item in lnq_Query)
                    {
                        lstDeclaraciones.Add(new vReg_Declaracion()
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdRegistro = Convert.ToInt32(item.IdRegistro),
                            NombreEmpresa = item.Empresa,
                            IdRecurso = item.IdRecurso,
                            DescripcionRecurso = item.Recurso,
                            NroReferenciaRecurso = item.NroReferenciaRecurso,
                            EstadoTrama = Convert.ToString(item.Estado)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
               "DADeclaracion.Listar", "No se pudo obtener registros. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                return null;
            }
            return lstDeclaraciones;
        }

        public vReg_Declaracion Get_Registro(string IdEmpresa, string IdRegistro)
        {
            var _Registro = new vReg_Declaracion();
            string
            strSql = "select    IdEmpresa,IdRegistro,IdCliente,IdRecurso,TipoDocumentoRecurso,NroReferenciaRecurso,DescripcionRecurso,Descripcion1,Descripcion2,Descripcion3,TipoRecurso, ";
            strSql += "         MesInformado,AnhoInformado,NroOrden,Observacion,Localidad,FechaIngreso,FechaEnvio,HoraEnvio,EnviadoPor,Estado,FechaRegistro,HoraRegistro,RegistradoPor, ";
            strSql += "         NombreArchivo,FlagPublicado,CantidadImagenes,FechaCierre,HoraCierre,CerradoPor ";
            strSql += "from	    REG_CONTROL ";
            strSql += "where	IdEmpresa='" + IdEmpresa + "' and IdRegistro=" + IdRegistro;

            DataTable dtResultado = SqlDataAccess.EjecutarQuery(strSql);
            if (dtResultado != null)
            {
                _Registro.IdEmpresa = dtResultado.Rows[0]["IdEmpresa"].ToString();
                _Registro.IdRegistro = Convert.ToInt32(dtResultado.Rows[0]["IdRegistro"].ToString());
                _Registro.IdCliente = dtResultado.Rows[0]["IdCliente"].ToString();
                _Registro.IdRecurso = dtResultado.Rows[0]["IdRecurso"].ToString();
                _Registro.TipoDocumentoReferencia = dtResultado.Rows[0]["TipoDocumentoRecurso"].ToString();
                _Registro.NroReferenciaRecurso = dtResultado.Rows[0]["NroReferenciaRecurso"].ToString();
                _Registro.DescripcionRecurso = dtResultado.Rows[0]["DescripcionRecurso"].ToString();
                _Registro.Descripcion001 = dtResultado.Rows[0]["Descripcion1"].ToString();
                _Registro.Descripcion002 = dtResultado.Rows[0]["Descripcion2"].ToString();
                _Registro.Descripcion003 = dtResultado.Rows[0]["Descripcion3"].ToString();
                _Registro.TipoRecurso = dtResultado.Rows[0]["TipoRecurso"].ToString();
                _Registro.MesInformado = dtResultado.Rows[0]["MesInformado"].ToString();
                _Registro.AnhoInformado = dtResultado.Rows[0]["AnhoInformado"].ToString();
                _Registro.NroOrden = dtResultado.Rows[0]["NroOrden"].ToString();
                _Registro.Observacion = dtResultado.Rows[0]["Observacion"].ToString();
                _Registro.Localidad = dtResultado.Rows[0]["Localidad"].ToString();
                if (!String.IsNullOrEmpty(dtResultado.Rows[0]["FechaIngreso"].ToString()))
                    _Registro.FechaIngreso = Convert.ToDateTime(dtResultado.Rows[0]["FechaIngreso"].ToString());
                _Registro.Estado = dtResultado.Rows[0]["Estado"].ToString();
                _Registro.FechaMaker = dtResultado.Rows[0]["FechaRegistro"].ToString();
                _Registro.HoraMaker = dtResultado.Rows[0]["HoraRegistro"].ToString();
                _Registro.Maker = dtResultado.Rows[0]["RegistradoPor"].ToString();
                _Registro.RegistroPublicado = Convert.ToBoolean(dtResultado.Rows[0]["FlagPublicado"].ToString());
                _Registro.FuenteOrigen = dtResultado.Rows[0]["NombreArchivo"].ToString();
            }
            else
                _Registro = null;
            return _Registro;
        }

        public List<vReg_Documento> Listar_Documentos(string IdEmpresa, string IdRegistro)
        {
            List<vReg_Documento> lDocumentos = new List<vReg_Documento>();
            try
            {
                using (DADeclaracionDataContext dc = new DADeclaracionDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_LISTAR_DECLARACIONES_DOCUMENTOS(IdEmpresa, Convert.ToInt32(IdRegistro));
                    foreach (var item in lnq_Query)
                    {
                        lDocumentos.Add(new vReg_Documento()
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdRegistro = Convert.ToInt32(item.IdRegistro),
                            NroSecuencia = Convert.ToInt32(item.NroSecuencia),
                            IdTipoServicio = Convert.ToString(item.IdTipoServicio),
                            TipoServicio = item.TipoServicio,
                            IdTipoDocumento = Convert.ToString(item.IdTipoDocumento),
                            DescripcionArchivo = item.DescripcionArchivo,
                            Riesgo = item.NivelRiesgo,
                            IdImagen = item.IdImagen,
                            ExisteEvidencia = item.Evidencia,
                            FechaDocumento = item.FechaDocumento,
                            FechaVencimiento = item.FechaVencimiento,
                            Detalle1 = item.Detalle,
                            EstadoDocumento = Convert.ToString(item.Estado)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
               "DADeclaracion.Listar_Documentos", "No se pudo obtener registros. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                return null;
            }
            return lDocumentos;
        }

        public List<vReg_DocumentoDato> Listar_DocumentosDatos(string IdEmpresa, string IdRegistro, string NroSecuencia)
        {
            List<vReg_DocumentoDato> lDocumentoDatos = new List<vReg_DocumentoDato>();
            try
            {
                string 
                    strSql = "select	";
                    strSql += "         rdd.IdEmpresa, rdd.IdRegistro, rdd.NroSecuencia, rdd.IdDato, dat.Descripcion, dat.Alias, ";
                    strSql += "         rdd.CategoriaDato, ISNULL(ValorTexto,'') AS ValorTexto, ISNULL(ValorDecimal,0.00) AS ValorDecimal, ISNULL(ValorFecha, GETDATE()) AS ValorFecha, ";
                    strSql += "         ISNULL(ValorEntero,0) AS ValorEntero, ISNULL(ValorBooleano,0) AS ValorBooleano, mae.Descripcion as Estado ";
                    strSql += "from	    REG_DOCUMENTODATO rdd ";
                    strSql += "         left join T_MAESTRO mae (NOLOCK) on rdd.Estado=mae.Valor and mae.CodigoTabla='90' ";
                    strSql += "         left join T_DATOS dat on rdd.IdDato=dat.IdDato ";
                    strSql += "where    rdd.IdEmpresa='" + IdEmpresa + "' and rdd.IdRegistro=" + IdRegistro + " and rdd.NroSecuencia=" + NroSecuencia;

                DataTable dtResultado = SqlDataAccess.EjecutarQuery(strSql);

                for (int i = 0; i < dtResultado.Rows.Count; i++)
                {
                    lDocumentoDatos.Add(new vReg_DocumentoDato()
                    {
                        IdEmpresa = dtResultado.Rows[i]["IdEmpresa"].ToString(),
                        IdRegistro = Convert.ToInt32(dtResultado.Rows[i]["IdRegistro"].ToString()),
                        NroSecuencia = Convert.ToInt32(dtResultado.Rows[i]["NroSecuencia"].ToString()),
                        IdDato = Convert.ToInt32(dtResultado.Rows[i]["IdDato"].ToString()),
                        DescripcionDato = dtResultado.Rows[i]["Descripcion"].ToString(),
                        DescripcionCorta = dtResultado.Rows[i]["Alias"].ToString(),
                        CategoriaDato = dtResultado.Rows[i]["CategoriaDato"].ToString(),

                        ValorDatoTexto =
                           dtResultado.Rows[i]["CategoriaDato"].ToString().Equals(Utilitario.TiposDatos.Texto) ?
                           Convert.ToString(dtResultado.Rows[i]["ValorTexto"]) : "",

                        ValorDatoEntero =
                            dtResultado.Rows[i]["CategoriaDato"].ToString().Equals(Utilitario.TiposDatos.Numerico) ?
                            Convert.ToInt32(dtResultado.Rows[i]["ValorEntero"].ToString()) : 0,
                        ValorDatoFecha =
                            dtResultado.Rows[i]["CategoriaDato"].ToString().Equals(Utilitario.TiposDatos.Fecha) ?
                            Convert.ToDateTime(dtResultado.Rows[i]["ValorFecha"].ToString()) : DateTime.Now,

                        ValorDatoFechaCadena =
                            dtResultado.Rows[i]["CategoriaDato"].ToString().Equals(Utilitario.TiposDatos.Fecha) ?
                            Convert.ToDateTime(dtResultado.Rows[i]["ValorFecha"].ToString()).ToString("dd/MM/yyyy"): DateTime.Now.ToString("dd/MM/yyyy"),

                        ValorDatoDecimal =
                            dtResultado.Rows[i]["CategoriaDato"].ToString().Equals(Utilitario.TiposDatos.Decimal) ?
                             Convert.ToDecimal(dtResultado.Rows[i]["ValorDecimal"].ToString()) : 0.00M,

                        ValorDatoBooleano =
                            dtResultado.Rows[i]["CategoriaDato"].ToString().Equals(Utilitario.TiposDatos.Booleano) ?
                            Convert.ToBoolean(dtResultado.Rows[i]["ValorBooleano"].ToString()) : false,
                    });
                }                
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
               "DADeclaracion.Listar_DocumentosDatos", "No se pudo obtener registros. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                return null;
            }
            return lDocumentoDatos;
        }

        public List<vReg_Imagen> Listar_Imagenes(string IdEmpresa, string IdRegistro, string NroSecuencia)
        {
            List<vReg_Imagen> lImagenes = new List<vReg_Imagen>();
            try
            {
                string
                strSql = "select	IdEmpresa,IdImagen,IdRegistro,NroSecuencia,NombreArchivo,PesoArchivo,NroArchivo,TotalPaginas,Estado, ";
                strSql += "         FechaRegistro,HoraRegistro,RegistradoPor ";
                strSql += "from	    Reg_Imagen ";
                strSql += "where    IdEmpresa='" + IdEmpresa + "' and IdRegistro=" + IdRegistro + " and NroSecuencia=" + NroSecuencia;

                DataTable dtResultado = SqlDataAccess.EjecutarQuery(strSql);

                for (int i = 0; i < dtResultado.Rows.Count; i++)
                {
                    lImagenes.Add(new vReg_Imagen()
                    {
                        IdEmpresa = dtResultado.Rows[i]["IdEmpresa"].ToString(),
                        IdImagen= dtResultado.Rows[i]["IdImagen"].ToString(),
                        IdRegistro = Convert.ToInt32(dtResultado.Rows[i]["IdRegistro"].ToString()),
                        NroSecuencia = Convert.ToInt32(dtResultado.Rows[i]["NroSecuencia"].ToString()),
                        NombreArchivo = dtResultado.Rows[i]["NombreArchivo"].ToString(),
                        PesoArchivo = Convert.ToInt32(dtResultado.Rows[i]["PesoArchivo"].ToString()),
                        NroArchivo = Convert.ToInt32(dtResultado.Rows[i]["NroArchivo"].ToString()),
                        Estado = dtResultado.Rows[i]["Estado"].ToString(),
                    });
                }
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
               "DADeclaracion.Listar_Imagenes", "No se pudo obtener registros. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                return null;
            }
            return lImagenes;
        }

        public vReg_Imagen Get_Imagen(string IdEmpresa, string sIdImagen)
        {
            var _Imagen = new vReg_Imagen();
            string
            strSql = "select    a.NroArchivo,a.Idimagen, a.NombreArchivo, ";
            strSql += "         right('0'+convert(varchar(2),MONTH(a.FechaRegistro)),2) as MesImagen, ";
            strSql += "         convert(varchar(4),year(a.FechaRegistro)) as AñoImagen, ";
            strSql += "         c.NroReferenciaRecurso as NumeroDoi , c.DescripcionRecurso as NombreRazon,a.PesoArchivo ";
            strSql += "from	    REG_IMAGEN a ";
            strSql += "         left join REG_DOCUMENTO b on a.IdEmpresa=b.IdEmpresa and a.IdRegistro = b.IdRegistro and a.NroSecuencia = b.NroSecuencia ";
            strSql += "         left join REG_CONTROL c on b.IdEmpresa=c.IdEmpresa and b.IdRegistro = c.IdRegistro ";
            strSql += "where	a.IdEmpresa='" + IdEmpresa + "' and a.Idimagen='" + sIdImagen + "' ";

            DataTable dtResultado = SqlDataAccess.EjecutarQuery(strSql);
            if (dtResultado != null)
            {
                _Imagen.NroArchivo = Convert.ToInt32(dtResultado.Rows[0]["NroArchivo"].ToString());
                _Imagen.IdImagen = dtResultado.Rows[0]["IdImagen"].ToString();
                _Imagen.NombreArchivo = dtResultado.Rows[0]["NombreArchivo"].ToString();
                _Imagen.NumeroDoi = dtResultado.Rows[0]["NumeroDoi"].ToString();
                _Imagen.NombreRazon = dtResultado.Rows[0]["NombreRazon"].ToString();
                _Imagen.MesImagen = dtResultado.Rows[0]["MesImagen"].ToString();
                _Imagen.AñoImagen = dtResultado.Rows[0]["AñoImagen"].ToString();
                _Imagen.PesoArchivo = Convert.ToInt32(dtResultado.Rows[0]["PesoArchivo"].ToString());
            }
            else
                _Imagen = null;
            return _Imagen;
        }

        public List<BEMaestro> Listar_OrdenesPorCliente(string IdCliente)
        {
            List<BEMaestro> lDocumentoDatos = new List<BEMaestro>();
            try
            {
                string strSql = "select	IdEmpresa, NroOrden from REG_ORDEN_SERVICIO where IdEmpresa='" + IdCliente + "' and Estado='P'";
                
                DataTable dtResultado = SqlDataAccess.EjecutarQuery(strSql);

                for (int i = 0; i < dtResultado.Rows.Count; i++)
                {
                    lDocumentoDatos.Add(new BEMaestro()
                    {
                        Codigo = dtResultado.Rows[i]["NroOrden"].ToString(),
                        Descripcion = dtResultado.Rows[i]["NroOrden"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
               "DADeclaracion.Listar_OrdenesPorCliente", "No se pudo obtener registros. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                return null;
            }
            return lDocumentoDatos;
        }

        //public void Asignar_Imagen(vReg_Documento objTipoDocumento,
        //    string sIdCliente, string sIdpedido, string sNroLinea, int nPaginas, string sRutaOrigen, string sRutaDestino)
        //{
        //    string sSql = "";
        //    using (SqlConnection sCn = new SqlConnection(Globales.ConfigServidor()))
        //    {
        //        sCn.Open();
        //        SqlTransaction sTrans = sCn.BeginTransaction();
        //        try
        //        {
        //            sSql = "select  right('000000'+convert(varchar(7),isnull(max(convert(numeric(7),idimagen))+1,1)),7) as idimagen from Reg_Imagen ";
        //            sSql += "where  IdEmpresa='" + objTipoDocumento.IdEmpresa + "' and IdRegistro= " + objTipoDocumento.IdRegistro + " and NroSecuencia=" + objTipoDocumento.NroSecuencia;
        //            string sIdimagen = Convert.ToString(SqlDataAccess.EjecutarComando(sSql));

        //            System.IO.FileInfo File = new System.IO.FileInfo(sRutaOrigen);
        //            int nKb = Convert.ToInt32(File.Length / 1024);

        //            sSql = "select  count(*)+1 from Reg_Imagen ";
        //            sSql += "where  IdEmpresa='" + objTipoDocumento.IdEmpresa + "' and IdRegistro= " + objTipoDocumento.IdRegistro + " and NroSecuencia=" + objTipoDocumento.NroSecuencia;
        //            int nNroArchivo = Convert.ToInt16(SqlHelper.ExecuteScalar(sTrans, CommandType.Text, sSql));

        //            sSql = "insert  into Reg_Imagen (IdEmpresa, IdImagen, IdRegistro, NroSecuencia, NombreArchivo, PesoArchivo, NroArchivo, TotalPaginas, Estado, FechaRegistro, HoraRegistro, RegistradoPor) values ( ";
        //            sSql += "       '" + objTipoDocumento.IdEmpresa + "', '" + sIdimagen + "', ";
        //            sSql += "       '" + objTipoDocumento.IdRegistro + "','" + objTipoDocumento.NroSecuencia + "','" + objTipoDocumento.Detalle1 + "', ";
        //            sSql += "       " + nNroArchivo.ToString() + "," + nPaginas.ToString() + ", ";
        //            sSql += "       '" + nKb.ToString() + "','ADemanda','" + sIdpedido + "','" + sNroLinea + "', 1, ";
        //            sSql += "       convert(varchar(10),getdate(),112),convert(varchar(8),getdate(),108),'" + objTipoDocumento.Maker + "','.pdf')  ";
        //            SqlDataAccess.EjecutarComando(sSql);

        //            sSql = "select  sum(TotalPaginas) from Reg_Imagen ";
        //            sSql += "where  IdCliente='" + sIdCliente + "' and IdPedido='" + sIdpedido + "' and NroLinea='" + sNroLinea + "' ";
        //            int nTotalImagenes = Convert.ToInt16(SqlHelper.ExecuteScalar(sTrans, CommandType.Text, sSql));

        //            if (oBE_Pro_PedidoDetalle.NroSecuencia == "")
        //            {
        //                sSql = "update  Reg_Legajo set  "; //si es file
        //                sSql += "       NroArchivos='" + nNroArchivo + "', ";
        //                sSql += "       IdPedidoImagen=case when IdPedidoImagen='' then '" + sIdpedido + "' else IdPedidoImagen end ";
        //                sSql += "where 	IdCliente = '" + sIdCliente + "' and TipoLegajo='" + oBE_Pro_PedidoDetalle.TipoLegajo + "' and CodigoIM='" + oBE_Pro_PedidoDetalle.CodigoIM + "'";
        //                SqlDataAccess.EjecutarComando(sTrans, CommandType.Text, sSql);
        //            }
        //            else
        //            {
        //                sSql = "update  Reg_Documento set  "; //si es documento
        //                sSql += "       NroArchivos=isnull(NroArchivos,0)+1, ";
        //                sSql += "       IdPedidoImagen='" + sIdpedido + "' ";
        //                sSql += "where 	IdCliente = '" + sIdCliente + "' and TipoLegajo='" + oBE_Pro_PedidoDetalle.TipoLegajo + "' and CodigoIM='" + oBE_Pro_PedidoDetalle.CodigoIM + "' and NroSecuencia='" + oBE_Pro_PedidoDetalle.NroSecuencia + "' ";
        //                SqlDataAccess.EjecutarComando(sTrans, CommandType.Text, sSql);
        //            }

        //            sSql = "update  Pro_PedidoDetalle set ";
        //            sSql += "       TotalImagenes='" + nTotalImagenes.ToString() + "', ";
        //            sSql += "       Estado='A', ";
        //            sSql += "       TipoAtencion='I', ";
        //            sSql += "       FechaProceso=CONVERT(varchar(8),GETDATE(),112), ";
        //            sSql += "       HoraProceso=CONVERT(varchar(8),GETDATE(),108), ";
        //            sSql += "       UsuarioProceso='" + sIdUsuario + "' ";
        //            sSql += "where 	IdCliente = '" + sIdCliente + "' and IdPedido='" + sIdpedido + "' and NroLinea='" + sNroLinea + "'  ";
        //            SqlDataAccess.EjecutarComando(sTrans, CommandType.Text, sSql);


        //            sRutaDestino = sRutaDestino + sIdimagen + ".pdf";
        //            System.IO.File.Copy(sRutaOrigen, sRutaDestino, false);
        //            sTrans.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            sTrans.Rollback();
        //            if (System.IO.File.Exists(sRutaDestino)) System.IO.File.Delete(sRutaDestino);
        //            throw ex;
        //        }
        //    }
        //}

        //public Boolean Quitar_Imagen(string sIdCliente, string sTipoLegajo, string sCodigoIM, string sNroSecuencia, string sIdimagen, string sExtensionImagen, string sImaMes, string sImaAño)
        //{
        //    using (SqlConnection sCn = new SqlConnection(ConexionDAO.sConexion))
        //    {
        //        sCn.Open();
        //        SqlTransaction sTrans = sCn.BeginTransaction();
        //        try
        //        {
        //            sSql = "select count(*) ";
        //            sSql += "from   Reg_Imagen where IdCliente = '" + sIdCliente + "' and TipoLegajo='" + sTipoLegajo + "' and CodigoIM='" + sCodigoIM + "' and NroSecuencia='" + sNroSecuencia + "' ";
        //            int nCant = Convert.ToInt16(SqlHelper.ExecuteScalar(sTrans, CommandType.Text, sSql));

        //            sSql = "delete from Reg_Imagen ";
        //            sSql += "where IdCliente = '" + sIdCliente + "' and TipoLegajo='" + sTipoLegajo + "' and CodigoIM='" + sCodigoIM + "' and NroSecuencia='" + sNroSecuencia + "' and Idimagen='" + sIdimagen + "'";
        //            SqlHelper.ExecuteNonQuery(sTrans, CommandType.Text, sSql);

        //            sSql = "update  Reg_Documento set  ";
        //            sSql += "       NroArchivos =" + (nCant - 1) + ", ";
        //            sSql += "       IdPedidoImagen='' ";
        //            sSql += "where 	IdCliente = '" + sIdCliente + "' and TipoLegajo='" + sTipoLegajo + "' and CodigoIM='" + sCodigoIM + "' and NroSecuencia='" + sNroSecuencia + "' ";
        //            SqlHelper.ExecuteNonQuery(sTrans, CommandType.Text, sSql);

        //            //string sDestino = Helper.fRutaImagenes(false) + sImaAño + "\\" + sImaMes + "\\" + sExtensionImagen;

        //            //Si hay imagen fisica, borrarla : 29-01-2013
        //            //if (System.IO.File.Exists(sDestino))
        //            //    System.IO.File.Delete(sDestino);


        //            sTrans.Commit();
        //            return true;
        //        }
        //        catch (Exception ex) { sTrans.Rollback(); return false; throw ex; }
        //    }
        //}

        public int ProcesarDocumento(int Opcion, vReg_Documento objDocumento, string sRutaOrigenImagen, string sRutaDestinoImagen)
        {
            var Lqn_Resultado = -1;
            try
            {
                bool? bPaso = null; int nKb = 0; string strNombreArchivo = "";
                //Asignar Imagen
                if (Opcion == 4)
                {
                    if (sRutaOrigenImagen != "")
                    {
                        System.IO.FileInfo File = new System.IO.FileInfo(sRutaOrigenImagen);
                        nKb = Convert.ToInt32(File.Length / 1024);
                        strNombreArchivo = File.Name;
                    }
                    using (DADeclaracionDataContext dc = new DADeclaracionDataContext(Globales.ConfigServidor()))
                    {
                        dc.CommandTimeout = 120;
                        Lqn_Resultado = dc.SP_UPD_DOCUMENTACION_MENSUAL(objDocumento.IdEmpresa, objDocumento.IdRegistro, objDocumento.NroSecuencia, objDocumento.FechaDesde,
                            objDocumento.Detalle1, strNombreArchivo, nKb, objDocumento.Maker, ref bPaso);

                        var Lqn_ResultadoDet = -1;
                        if (Lqn_Resultado == 0)
                        {
                            if (objDocumento.lDetalleDocumentos != null)
                            {
                                foreach (var dato in objDocumento.lDetalleDocumentos)
                                {
                                    Lqn_ResultadoDet = dc.SP_UPD_DOCUMENTACIONDATO_MENSUAL(dato.IdEmpresa, dato.IdRegistro, dato.NroSecuencia, dato.IdDato, dato.ValorDatoTexto,
                                        dato.ValorDatoEntero, dato.ValorDatoDecimal, dato.ValorDatoFecha, dato.ValorDatoBooleano, objDocumento.Maker, ref bPaso);

                                    Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "", "DADeclaracion.MantenerRegistroDocumento", "Ejecutado", NivelMensajeLog.NINGUNO);
                                }
                            }
                        }
                    }

                    if (Lqn_Resultado == 0)
                    {
                        System.IO.File.Copy(sRutaOrigenImagen, sRutaDestinoImagen, false);
                        if (System.IO.File.Exists(sRutaOrigenImagen)) System.IO.File.Delete(sRutaOrigenImagen);
                    }
                }

                //Quitar Imagen
                if (Opcion == 5)
                {
                    //string sDestino = Utilitario.fRutaImagenes(false) + sImaAño + "\\" + sImaMes + "\\" + sExtensionImagen;
                    //Si hay imagen fisica, borrarla
                    //if (System.IO.File.Exists(sDestino))
                    //    System.IO.File.Delete(sDestino);
                }

                return Lqn_Resultado == 0 ? 1 : 0;
            }
            catch (Exception ex)
            {
                if (Opcion == 4)
                    if (System.IO.File.Exists(sRutaDestinoImagen)) System.IO.File.Delete(sRutaDestinoImagen);
                throw ex;
            }
        }
    }
}
