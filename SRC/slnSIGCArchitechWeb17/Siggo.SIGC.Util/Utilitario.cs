using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Globalization;
using System.Security.Cryptography;



namespace Siggo.SIGC.Util
{
    public class Utilitario
    {
        public const int FLAG_APLICA_SEG_PWD = 1;
        public const string USUARIO_MASTER = "admin";
        public static string EMPRESA_RUC = "SIGGO";

        public static string Logo(string sIdcliente, string username)
        {
            string sLogo = "";
            
            if (sIdcliente == "")
            {
                sLogo = "~/Images/logo_blanco.png";
            }
            else
            {
                switch (sIdcliente)
                {
                    case "SG001": sLogo = "~/Images/logo_blanco.png"; break;                    
                    case "SG002": sLogo = "~/Images/logo_repsol.png"; break;
                    case "DEMO": sLogo = "~/Images/Logo_demo.jpg"; break;
                    default:
                        sLogo = "~/Images/logo_blanco.png"; break;
                }
            }

            return sLogo;
        }
        public static string GetWeekOfYear(DateTime dtPassed)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(dtPassed, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return Convert.ToString(weekNum);
        }        
        public static DataTable Importar(string sRuta ,string sNombre, string sExtension)
        {
            try
            {
                DataTable dt = new DataTable();

                #region Trabajando con CSV

                if (sExtension == "csv")
                {
                    string sCn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + sRuta.Replace(sNombre + "." + sExtension, "") + "';" + "Extended Properties=Text";

                    DataTable dt1 = new DataTable();
                    using (OleDbConnection conn = new OleDbConnection(sCn))
                    using (OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [" + sNombre + "#csv]", conn))
                    { da.Fill(dt1); }
                    return dt1;

                }

                #endregion

                #region Trabajando con TXT

                if (sExtension == "txt")
                {
                    DataTable dt1 = new DataTable();
                    dt1.Columns.Add("F1", typeof(string));
                    using (StreamReader sr = new StreamReader(sRuta, Encoding.GetEncoding("iso-8859-1")))
                    {
                        string sLinea = "";
                        while (sLinea != null)
                        {
                            sLinea = sr.ReadLine();
                            if (sLinea != null) dt1.Rows.Add(sLinea);
                        }
                    }
                    return dt1;
                }

                #endregion

                #region Trabajando con XLS y XLSX

                if (sExtension == "xls" || sExtension == "xlsx")
                {
                    /* string sCn = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sRuta + ";" + "Extended Properties=Excel 8.0";*/
                    string sCn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + sRuta + ";" + "Extended Properties=Excel 12.0";
                    using (OleDbConnection conn = new OleDbConnection(sCn))
                    using (OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [Legajo$]", conn))
                    { da.Fill(dt); }
                }
                //this.Cursor = Cursors.WaitCursor;
                //dt.Dispose(); dt = null;

                #endregion

                return dt;
            }
            catch (Exception ex) { throw ex; }

        }
        public static byte[] Generar_Excel(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table border='" + "2px" + "'b>");
            sb.Append("<tr>");
            foreach (System.Data.DataColumn dc in dt.Columns)
            {
                sb.Append("<td><b><font face=Arial size=2>" + dc.ColumnName + "</font></b></td>");
            }
            sb.Append("</tr>");

            foreach (System.Data.DataRow dr in dt.Rows)
            {
                sb.Append("<tr>");
                foreach (System.Data.DataColumn dc in dt.Columns)
                {
                    sb.Append("<td><font face=Arial size=" + "14px" + ">" + (dr[dc].ToString() == "0" ? "" : dr[dc].ToString()) + "</font></td>");
                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");

            //Response.AddHeader("Content-Disposition", "Incompletos.xls");
            //Response.ContentType = "application/vnd.ms-excel";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            dt.Dispose();
            return buffer;
        }        
        public static byte[] GetBytesFromFile(string fullFilePath)
        {
            // this method is limited to 2^32 byte files (4.2 GB)

            FileStream fs = null;
            try
            {
                fs = File.OpenRead(fullFilePath);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                return bytes;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }

        }

        public static string GenerarUserID()
        {
            //AR54001
            Random rnd = new Random();
            int a1 = rnd.Next(10, 20);
            int a2 = rnd.Next(21, 30);
            int a3 = rnd.Next(10, 40);

            int a4 = rnd.Next(65, 91);
            int a5 = rnd.Next(65, 91);
            string l1 = Convert.ToChar(a4).ToString();
            string l2 = Convert.ToChar(a5).ToString();

            string sNuevoUser = string.Concat(l1, l2, a2, a3);
            return sNuevoUser;
        }

        #region Utilitarios

        public static DateTime ConvertirFechaTextoToDatetime(string sFecha)
        {
            if (sFecha != "")
                return Convert.ToDateTime(sFecha);
            else
                return Convert.ToDateTime("01/01/1900");
        }
        public static string Left(string param, int length)
        {
            //we start at 0 since we want to get the characters starting from the
            //left and with the specified lenght and assign it to a variable
            string result = param.Substring(0, length);
            //return the result of the operation
            return result;
        }
        public static string Right(string param, int length)
        {
            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }
        public static string GetFecha_YYMMDD(DateTime dt_Datetime)
        {
            var sAno = dt_Datetime.Year.ToString().Substring(2, 2);
            var sMes = String.Format("{0:00}", dt_Datetime.Month);
            var sDia = String.Format("{0:00}", dt_Datetime.Day);
            var str_Datetime = sAno + sMes + sDia;
            return str_Datetime.Trim();
        }
        public static string GetFecha_DDMMYYYY(DateTime dt_Datetime)
        {
            var sAno = dt_Datetime.Year.ToString();
            var sMes = String.Format("{0:00}", dt_Datetime.Month);
            var sDia = String.Format("{0:00}", dt_Datetime.Day);
            var str_Datetime = string.Concat(sDia, "/", sMes, "/", sAno);
            return str_Datetime.Trim();
        }

        public static string fRutaImagenes(Boolean bGenerar)
        {
            try
            {
                string sRuta = "";
                sRuta = @"D:\Imagenes\" + Utilitario.EMPRESA_RUC;
                if (bGenerar)
                {
                    string sAño = DateTime.Now.Year.ToString();
                    string sMes = Utilitario.Right("0" + DateTime.Now.Month.ToString(), 2);

                    sRuta += "\\" + sAño + "\\" + sMes;
                    if (System.IO.Directory.Exists(sRuta) == false)
                    {
                        System.IO.Directory.CreateDirectory(sRuta);
                    }
                }
                return sRuta + "\\";
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
        }

        #region Seguridad

        public static string EncryptText(string strText)
        {
            return Encrypt(strText, "&%#*@,:?");
        }
        public static string DecryptText(string strText)
        {
            return Decrypt(strText, "&%#*@,:?");
        }

        private static string Decrypt(string stringToDecrypt, string sEncryptionKey)
        {
            byte[] key = { };
            byte[] IV = { 0x12, 0xCD, 0x56, 0x34, 0x90, 0xAB, 0xEF, 0x78 };
            byte[] inputByteArray = new byte[stringToDecrypt.Length];

            key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(stringToDecrypt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }

        private static string Encrypt(string stringToEncrypt, string sEncryptionKey)
        {
            byte[] key = { };
            byte[] IV = { 0x12, 0xCD, 0x56, 0x34, 0x90, 0xAB, 0xEF, 0x78 };
            key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }

        #endregion

        public struct TiposDatos
        {
            public const string Texto = "TipoCadena";
            public const string Numerico = "TipoNumero";
            public const string Decimal = "TipoDecimal";
            public const string Fecha = "TipoFecha";
            public const string Booleano = "TipoBooleano";
        }

    }

    public static class MC
    {
        /// <summary>
        /// Array de los casos que existen en la funcionalidad de maker and checker
        /// </summary>
        static string[] str_mc = new string[]
        {
            "I,Pendiente,Creación,P",
            "U,Pendiente,Modificación,P",
            "C,Pendiente,Eliminación,P",
            "I,Autorizado,Creación,A",
            "U,Autorizado,Modificación,A",
            "C,Autorizado,Eliminación,A",

            //orden de servicio
            "I,En Proceso, ,R",
            "U,En Proceso, ,R"
        };

        /// <summary>
        /// Lista que retorna los casos de maker y checker
        /// </summary>
        /// <returns></returns>
        public static List<var_mc> lst_mc()
        {
            List<var_mc> _lst_mc = new List<var_mc>();
            _lst_mc.Clear();
            foreach (string _str in str_mc)
            {
                var data = _str.Split(',');

                _lst_mc.Add(new var_mc()
                {
                    Estado = data[3],
                    DescEstado = data[1],
                    Accion = data[0],
                    DescAccion = data[2]
                });

            }
            return _lst_mc;
        }

        public static string get_desc_mk(string pEstado, string pAccion)
        {
            var str_estado = lst_mc().Where(x => x.Estado.Trim() == pEstado.Trim()).ToList().Count > 0 ?
                lst_mc().Where(x => x.Estado.Trim() == pEstado.Trim()).ToList()[0].DescEstado : "";

            var srt_Accion = lst_mc().Where(x => x.Accion.Trim() == pAccion.Trim()).ToList().Count > 0 ?
                lst_mc().Where(x => x.Accion.Trim() == pAccion.Trim()).ToList()[0].DescAccion : "";

            return srt_Accion + " " + str_estado;
        }

    }
    public class var_mc
    {
        public string Estado { get; set; }
        public string DescEstado { get; set; }
        public string Accion { get; set; }
        public string DescAccion { get; set; }

    }
}