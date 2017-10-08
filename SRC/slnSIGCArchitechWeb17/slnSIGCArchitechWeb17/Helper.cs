using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using slnSIGCArchitechWeb17.Models;

namespace slnSIGCArchitechWeb17
{
    public class Helper
    {

        public static List<ComunModel> ActividadNormal()
        {
            List<ComunModel> _Acciones = new List<ComunModel>();
            _Acciones.Add(new ComunModel("1", "Si"));
            _Acciones.Add(new ComunModel("0", "No"));
            return _Acciones;
        }

        public static List<ComunModel> RecibeNotificaciones()
        {
            List<ComunModel> _TiposAcceso = new List<ComunModel>();
            _TiposAcceso.Add(new ComunModel("S", "SI"));
            _TiposAcceso.Add(new ComunModel("N", "NO"));
            return _TiposAcceso;
        }

        public static List<ComunModel> Acciones()
        {
            List<ComunModel> _Acciones = new List<ComunModel>();
            _Acciones.Add(new ComunModel("True", "Si"));
            _Acciones.Add(new ComunModel("False", "No"));
            return _Acciones;
        }

        public static List<ComunModel> TipoDocs()
        {
            List<ComunModel> _TiposDocs = new List<ComunModel>();
            _TiposDocs.Add(new ComunModel("1", "LE"));
            _TiposDocs.Add(new ComunModel("2", "DNI"));
            _TiposDocs.Add(new ComunModel("3", "LM"));
            _TiposDocs.Add(new ComunModel("4", "PASAPORTE"));
            _TiposDocs.Add(new ComunModel("5", "CE"));
            _TiposDocs.Add(new ComunModel("6", "RUC"));

            return _TiposDocs;
        }

        public static List<ComunModel> TipoDato()
        {
            List<ComunModel> _TipoDato = new List<ComunModel>();
            _TipoDato.Add(new ComunModel("TipoCadena", "Texto"));
            _TipoDato.Add(new ComunModel("TipoNumero", "Número"));
            _TipoDato.Add(new ComunModel("TipoDecimal", "Decimal"));
            _TipoDato.Add(new ComunModel("TipoFecha", "Fecha"));
            _TipoDato.Add(new ComunModel("TipoBooleano", "Booleano"));
            return _TipoDato;
        }

        public static List<ComunModel> Llenar_Meses()
        {
            var lMes = new List<ComunModel>();

            for (int i = 1; i <= 12; i++)
            {
                ComunModel _Mes = new ComunModel();
                _Mes.Codigo = i.ToString();
                switch (i)
                {
                    case 1: _Mes.Descripcion = "Enero"; break;
                    case 2: _Mes.Descripcion = "Febrero"; break;
                    case 3: _Mes.Descripcion = "Marzo"; break;
                    case 4: _Mes.Descripcion = "Abril"; break;
                    case 5: _Mes.Descripcion = "Mayo"; break;
                    case 6: _Mes.Descripcion = "Junio"; break;
                    case 7: _Mes.Descripcion = "Julio"; break;
                    case 8: _Mes.Descripcion = "Agosto"; break;
                    case 9: _Mes.Descripcion = "Setiembre"; break;
                    case 10: _Mes.Descripcion = "Octubre"; break;
                    case 11: _Mes.Descripcion = "Noviembre"; break;
                    case 12: _Mes.Descripcion = "Diciembre"; break;
                }
                lMes.Add(_Mes);

            }
            return lMes;
        }

        public static List<string> Llenar_Años()
        {
            try
            {
                var lAños = new List<string>();
                int nAñoini = 2015;
                int nAñofin = DateTime.Now.Year + 1;
                int nAños = (nAñofin - nAñoini) + 1;
                System.Object[] ItemObject = new System.Object[nAños];
                for (int i = 0; i < nAños; i++)
                {
                    lAños.Add(nAñoini.ToString());
                    nAñoini += 1;
                }
                return lAños;
            }
            catch (Exception ex) { throw ex; }
        }

        public struct TiposDatos
        {
            public const string Texto = "TipoCadena";
            public const string Numerico = "TipoNumero";
            public const string Decimal = "TipoDecimal";
            public const string Fecha = "TipoFecha";
            public const string Booleano = "TipoBooleano";
        }

        public struct Roles
        {
            public const string ADMINISTRADOR = "1";
            public const string OPERADOR = "2";
            public const string AUDITOR = "3";
        }

    }
}