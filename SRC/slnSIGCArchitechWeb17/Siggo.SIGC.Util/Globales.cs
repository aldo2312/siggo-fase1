using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;

namespace Siggo.SIGC.Util
{
    public static class Globales
    {
        public static string USUARIO_ACTUAL = string.Empty;
        public static string TITULO_APLICACION = "SigcWeb";
        
        #region Cadena_Conexion_BD
        public static string sDBservidor = @"LAP-LENOVO";
        public static string sDBusuario = "sa";
        public static string sDBPassword = "09346670";
        public static string sDBnombre = "SiggoData";

        public static string CADENA_CNX_SIGGODATA = string.Empty;
        #endregion

        public static int DIAS_RENOVAR_PWD = 90;

        public static string ConfigServidor()
        {
            try
            {
                CADENA_CNX_SIGGODATA = "Server=" + sDBservidor + ";Uid=" + sDBusuario + ";Pwd=" + sDBPassword + ";Database=" + sDBnombre;
            }
            catch (Exception ex) { throw ex; }
            return CADENA_CNX_SIGGODATA;
        }

    }
}
