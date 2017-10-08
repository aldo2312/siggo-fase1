using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Siggo.SIGC.Entity;
using Siggo.SIGC.Util;

namespace Siggo.SIGC.DataAccess
{
    public class DAEmpresa
    {
        public BEEmpresa ObtenerEmpresa(string IdEmpresa)
        {
            BEEmpresa retorno = null;
            try
            {
                using (DAEmpresaDataContext dc = new DAEmpresaDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_OBTENER_DATOS_EMPRESA(IdEmpresa).ToArray();
                    if (lnq_Query.Count() > 0)
                    {
                        retorno = new BEEmpresa();
                        var item = lnq_Query.First();                        
                        retorno.IdEmpresa = item.IdEmpresa;
                        retorno.RazonSocial = item.RazonSocial;
                        retorno.Direccion = item.Direccion;
                        retorno.PrefijoCorreo = item.PrefijoCorreo;
                        retorno.Correo = item.Correo;
                        retorno.Contacto = item.Contacto;
                        retorno.RucEmpresa = item.RucEmpresa;
                        retorno.Telefono = item.Telefono;
                        retorno.TipoEmpresa = item.TipoEmpresa;
                        retorno.TipoEmpresaSiggo = item.TipoEmpresaSiggo;
                        retorno.NotificacionDiasVencimiento = item.DiasNotificacionVencimiento;
                        retorno.ActividadNormalEspecifica = item.ActividadNormalEspecifica;
                        retorno.IdCliente = item.IDCliente;
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

        public List<BEEmpresa> Listar(string Id, string Nombre, string TipoEmpresa, string TipoEmpresaSiggo, string Usuario)
        {
            List<BEEmpresa> lEmpresas = new List<BEEmpresa>();
            try
            {
                using (DAEmpresaDataContext dc = new DAEmpresaDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_LISTAR_EMPRESAS(Id, Nombre, TipoEmpresa, TipoEmpresaSiggo, Usuario);
                    foreach (var item in lnq_Query)
                    {
                        lEmpresas.Add(new BEEmpresa()
                        {
                            IdEmpresa = item.IDEMPRESA,
                            RazonSocial = item.RAZONSOCIAL,
                            TipoEmpresa = item.DESC_TIPOEMPRESA,                            
                            RucEmpresa = item.RUCEMPRESA,
                            EmpresaAsociada = item.EmpresaAsociada,
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
            return lEmpresas;
        }

        public int MantenerEmpresa(int Opcion, BEEmpresa oEmpresa)
        {
            try
            {
                using (DAEmpresaDataContext dc = new DAEmpresaDataContext(Globales.ConfigServidor()))
                {
                    var Lqn_Resultado = dc.SP_MANT_REG_EMPRESA(Opcion, 
                        oEmpresa.IdEmpresa, 
                        oEmpresa.RazonSocial, 
                        oEmpresa.Direccion, 
                        oEmpresa.PrefijoCorreo,
                        oEmpresa.Telefono, 
                        oEmpresa.Correo, 
                        oEmpresa.Contacto, 
                        oEmpresa.TipoEmpresa, 
                        oEmpresa.RucEmpresa, 
                        oEmpresa.NotificacionDiasVencimiento,
                        oEmpresa.ActividadNormalEspecifica, 
                        oEmpresa.TipoEmpresaSiggo, 
                        oEmpresa.IdCliente, 
                        oEmpresa.Maker);
                    return Lqn_Resultado == 0 ? 1 : 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BEMaestro> ObtenerTiposEmpresa()
        {
            List<BEMaestro> lTiposEmpresa = new List<BEMaestro>();
            try
            {
                using (DAMaestroDataContext dc = new DAMaestroDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_OBTENER_LISTA_MAESTRO("20");
                    foreach (var item in lnq_Query)
                    {
                        lTiposEmpresa.Add(new BEMaestro()
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
            return lTiposEmpresa;
        }
    }
}
