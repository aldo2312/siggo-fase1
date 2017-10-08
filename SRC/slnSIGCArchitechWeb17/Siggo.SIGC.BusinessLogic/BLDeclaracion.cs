using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Siggo.SIGC.DataAccess;
using Siggo.SIGC.Entity;

namespace Siggo.SIGC.BusinessLogic
{
    public class BLDeclaracion
    {
        public List<vReg_Declaracion> Listar(string IdEmpresa, string IdCliente, string NroOrden, string TipoRecurso, string EstadoRegistro, string MesControl, string AnhoControl, string IdUsuario, string IdRegistro)
        {
            return new DADeclaracion().Listar(IdEmpresa, IdCliente, NroOrden, TipoRecurso, EstadoRegistro, MesControl, AnhoControl, IdUsuario, IdRegistro);
        }

        public vReg_Declaracion Get_Registro(string IdEmpresa, string IdRegistro)
        {
            return new DADeclaracion().Get_Registro(IdEmpresa, IdRegistro);
        }

        public List<vReg_Documento> Listar_Documentos(string IdEmpresa, string IdRegistro)
        {
            return new DADeclaracion().Listar_Documentos(IdEmpresa, IdRegistro);
        }

        public List<vReg_DocumentoDato> Listar_DocumentosDatos(string IdEmpresa, string IdRegistro, string NroSecuencia)
        {
            return new DADeclaracion().Listar_DocumentosDatos(IdEmpresa, IdRegistro, NroSecuencia);
        }

        public List<vReg_Imagen> Listar_Imagenes(string IdEmpresa, string IdRegistro, string NroSecuencia)
        {
            return new DADeclaracion().Listar_Imagenes(IdEmpresa, IdRegistro, NroSecuencia);
        }

        public vReg_Imagen Get_Imagen(string IdEmpresa, string sIdImagen)
        {
            return new DADeclaracion().Get_Imagen(IdEmpresa, sIdImagen);
        }

        public List<BEMaestro> Listar_OrdenesPorCliente(string IdCliente)
        {
            return new DADeclaracion().Listar_OrdenesPorCliente(IdCliente);
        }

        public int ProcesarDocumento(int Opcion, vReg_Documento objDocumento, string sRutaOrigenImagen, string sRutaDestinoImagen)
        {
            return new DADeclaracion().ProcesarDocumento(Opcion, objDocumento, sRutaOrigenImagen, sRutaDestinoImagen);
        }
    }
}
