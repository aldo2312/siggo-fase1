using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Siggo.SIGC.DataAccess;
using Siggo.SIGC.Entity;

namespace Siggo.SIGC.BusinessLogic
{
    public class BLOrdenServicio
    {
        public List<BEOrdenServicio> Listar(string IdEmpresa, string IdUsuario, string TipoEmpresaSiggo)
        {
            return new DAOrdenServicio().Listar(IdEmpresa, IdUsuario, TipoEmpresaSiggo);
        }
        public List<BEOrdenServicio> Listar(string NroOrden, string Contratista, string Cliente, DateTime FechaDesde, DateTime FechaHasta, string IdUsuario)
        {
            return new DAOrdenServicio().Listar(NroOrden, Contratista, Cliente, FechaDesde, FechaHasta, IdUsuario);
        }

        public List<BEOrdenServicioAreas> ObtenerAreas(string IdEmpresa, string NroOrden)
        {
            return new DAOrdenServicio().ObtenerAreas(IdEmpresa, NroOrden);
        }

        public List<BEOrdenServicioLocalizaciones> ObtenerSedes(string IdEmpresa, string NroOrden)
        {
            return new DAOrdenServicio().ObtenerSedes(IdEmpresa, NroOrden);
        }

        public List<BEOrdenServicioContratas> ObtenerContratistas(string IdEmpresa, string NroOrden)
        {
            return new DAOrdenServicio().ObtenerContratistas(IdEmpresa, NroOrden);
        }

        public List<BEMaestro> ObtenerAreas()
        {
            return new DAOrdenServicio().ObtenerAreas();
        }

        public List<BEMaestro> ObtenerSedes()
        {
            return new DAOrdenServicio().ObtenerSedes();
        }

        public List<BEEmpresa> ObtenerContratas(string Id, string Usuario)
        {
            return new DAOrdenServicio().ObtenerContratas(Id, Usuario);
        }

        public BEOrdenServicio ObtenerOS(string IdEmpresa, string NroOrden)
        {
            return new DAOrdenServicio().ObtenerOS(IdEmpresa, NroOrden);
        }

        public int MantenerOrdenServicio(int Opcion, BEOrdenServicio oOrdenServicio, out string strNroOrdenRetorno)
        {
            return new DAOrdenServicio().MantenerOrdenServicio(Opcion, oOrdenServicio, out strNroOrdenRetorno);
        }

        public void MantenerOrdenServicioAreas(int Opcion, List<BEOrdenServicioAreas> lAreas)
        {
            new DAOrdenServicio().MantenerOrdenServicioAreas(Opcion, lAreas);
        }

        public void MantenerOrdenServicioLocalizaciones(int Opcion, List<BEOrdenServicioLocalizaciones> lLocalizaciones)
        {
            new DAOrdenServicio().MantenerOrdenServicioLocalizaciones(Opcion, lLocalizaciones);
        }

        public void MantenerOrdenServicioContratas(int Opcion, List<BEOrdenServicioContratas> lContratas)
        {
            new DAOrdenServicio().MantenerOrdenServicioContratas(Opcion, lContratas);
        }

        public List<BEOrdenServicioPeriodos> ListarPeriodos(string NroOrden, string Contratista, string Cliente, string Mes, string Anho)
        {
            return new DAOrdenServicio().ListarPeriodos(NroOrden, Contratista, Cliente, Mes, Anho);
        }

        public void ProcesarPeriodoDeclaracion(string NroOrden, string Contratista, string Cliente, string Mes, string Anho, bool bProcesar)
        {
            new DAOrdenServicio().ProcesarPeriodoDeclaracion(NroOrden, Contratista, Cliente, Mes, Anho, bProcesar);
        }       

        public int GenerarPeriodoDeclaracion(string IdEmpresa, string NroOrden, string Mes, string Anho, string RegistradoPor)
        {
            return new DAOrdenServicio().GenerarPeriodoDeclaracion(IdEmpresa, NroOrden, Mes, Anho, RegistradoPor);
        }

        public int QuitarPeriodoDeclaracion(List<BEOrdenServicioPeriodos> lstRequisitos)
        {
            return new DAOrdenServicio().QuitarPeriodoDeclaracion(lstRequisitos);
        }

        public bool ExistePeriodoDeclaracion(string IdEmpresa, string NroOrden, string Mes, string Anho)
        {
            return new DAOrdenServicio().ExistePeriodoDeclaracion(IdEmpresa, NroOrden, Mes, Anho);
        }
    }
}
