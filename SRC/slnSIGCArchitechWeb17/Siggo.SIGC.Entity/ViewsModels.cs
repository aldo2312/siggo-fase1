using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siggo.SIGC.Entity
{
    public class vReg_Declaracion : BEMantenimientoBase
    {
        public string IdEmpresa { get; set; }
        public int IdRegistro { get; set; }
        public string IdCliente { get; set; }
        public string NroOrden { get; set; }
        public string IdRecurso { get; set; }
        public string NroReferenciaRecurso { get; set; }
        public string DescripcionRecurso { get; set; }
        public string TipoRecurso { get; set; }
        public string Observacion { get; set; }
        public string EstadoTrama { get; set; }
        public string NombreEmpresa { get; set; }

        public string MesInformado { get; set; }
        public string AnhoInformado { get; set; }
        public string Localidad { get; set; }
        public bool RegistroPublicado { get; set; }
        public string TipoDocumentoReferencia { get; set; }
        public string Descripcion001 { get; set; }
        public string Descripcion002 { get; set; }
        public string Descripcion003 { get; set; }
        public string FuenteOrigen { get; set; }
        public DateTime FechaIngreso { get; set; }
    }

    public class vReg_Documento : BEMantenimientoBase
    {
        public string IdEmpresa { get; set; }
        public int IdRegistro { get; set; }
        public int NroSecuencia { get; set; }
        public string IdTipoServicio { get; set; }
        public string TipoServicio { get; set; }
        public string IdTipoDocumento { get; set; }
        public string DescripcionArchivo { get; set; }
        public string Riesgo { get; set; }
        public string Frecuencia { get; set; }
        public string EstadoDocumento { get; set; }
        public string Detalle1 { get; set; }
        public string Detalle2 { get; set; }
        public string Detalle3 { get; set; }
        public string Detalle4 { get; set; }
        public DateTime? FechaDesde { get; set; } //Presentacion
        public DateTime? FechaHasta { get; set; } //Vencimiento
        
        public List<vReg_DocumentoDato> lDetalleDocumentos { get; set; }
        public List<vReg_Imagen> lImagenes { get; set; }

        public string FechaDocumento { get; set; }
        public string FechaVencimiento { get; set; }
        public string IdImagen { get; set; }
        public string ExisteEvidencia { get; set; }
    }

    public class vReg_DocumentoDato : BEMantenimientoBase
    {
        public string IdEmpresa { get; set; }
        public int IdRegistro { get; set; }
        public int NroSecuencia { get; set; }
        public int IdDato { get; set; }
        public string DescripcionDato { get; set; }
        public string DescripcionCorta { get; set; }
        public string CategoriaDato { get; set; }
        public string ValorDatoTexto { get; set; }
        public int ValorDatoEntero { get; set; }
        public DateTime ValorDatoFecha { get; set; }
        public decimal ValorDatoDecimal { get; set; }
        public bool ValorDatoBooleano { get; set; }

        public string ValorDatoFechaCadena { get; set; }
    }

    public class vReg_Imagen : BEMantenimientoBase
    {
        public string IdEmpresa { get; set; }
        public int IdRegistro { get; set; }
        public int NroSecuencia { get; set; }
        public string IdImagen { get; set; }
        public string NombreArchivo { get; set; }
        public int PesoArchivo { get; set; }
        public int NroArchivo { get; set; }

        public string NumeroDoi { get; set; }
        public string NombreRazon { get; set; }
        public string MesImagen { get; set; }
        public string AñoImagen { get; set; }
    }
}
