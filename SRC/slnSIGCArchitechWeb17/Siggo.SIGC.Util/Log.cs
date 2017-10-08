using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siggo.SIGC.Util
{
    public class Log
    {
        public static void EscribirLog(TipoLog prmTipoLog, ThreadSistema prmThreadOrigen, string prmIdSeguimiento,
                                        string prmMetodo, string prmContenido, NivelMensajeLog prmNivelMensajeLog)
        {

            string strSeguimientoHabilitado = ConfigurationManager.AppSettings["LOG_MODO"];
            bool blnLogDetallado = false;
            string strDirectorioLog = ConfigurationManager.AppSettings["LOG_RUTA"];
            string strNombreLog = ConfigurationManager.AppSettings["LOG_NOMBRE"];
            int nTamanhoIdSeguimiento = 15;
            StackTrace stackTrace = new StackTrace();
            string strMetodosInvocados = string.Empty;

            if (prmNivelMensajeLog == NivelMensajeLog.NOTIFICACION)
            {
                for (int i = 0; i < stackTrace.FrameCount; i++)
                {
                    strMetodosInvocados += stackTrace.GetFrame(i).GetMethod().Name + "(" + stackTrace.GetFrame(i).GetFileLineNumber().ToString() + ") --> ";
                }
            }


            #region Validaciones

            //Recortando el ID si es demasiado grande
            if (!String.IsNullOrEmpty(prmIdSeguimiento))
            {
                if (prmIdSeguimiento.Length > nTamanhoIdSeguimiento)
                {
                    prmIdSeguimiento = prmIdSeguimiento.Substring(0, nTamanhoIdSeguimiento);
                }
            }

            if (!String.IsNullOrEmpty(strSeguimientoHabilitado))
            {
                if (strSeguimientoHabilitado.ToLower().Equals("f"))
                {
                    blnLogDetallado = true;
                }
            }

            if (String.IsNullOrEmpty(strNombreLog)) return;

            if (String.IsNullOrEmpty(strDirectorioLog) || !Directory.Exists(strDirectorioLog)) return;

            if (prmTipoLog == TipoLog.Detallado)
            {
                if (!blnLogDetallado) return;
            }

            strNombreLog = DateTime.Today.ToString("yyyyMMdd") + strNombreLog + ".txt";

            #endregion

            #region Contenido

            String strHora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            String strThread = prmThreadOrigen.ToString("G");
            String strNivel = "";

            switch (prmNivelMensajeLog)
            {
                case NivelMensajeLog.NINGUNO:
                    strNivel = "SEGUIMIENTO ";
                    break;
                case NivelMensajeLog.ERROR:
                    strNivel = "***ERROR*** ";
                    break;
                case NivelMensajeLog.NOTIFICACION:
                    strNivel = "NOTIFICACION";
                    break;
                default:
                    break;
            }

            StringBuilder sbContenido = new StringBuilder();

            sbContenido.Append("\r\n");
            sbContenido.Append(prmIdSeguimiento.PadRight(nTamanhoIdSeguimiento, ' '));
            sbContenido.Append(";");
            sbContenido.Append(strHora);
            sbContenido.Append(";");
            sbContenido.Append(strThread);
            sbContenido.Append(";");
            sbContenido.Append(strNivel);
            sbContenido.Append(";");
            sbContenido.Append(prmMetodo);
            sbContenido.Append(";[");
            if (prmNivelMensajeLog == NivelMensajeLog.NOTIFICACION)
            {
                sbContenido.Append("Stackstrace: ");
                sbContenido.AppendLine(strMetodosInvocados);
                sbContenido.Append(prmContenido);
            }
            else
                sbContenido.Append(prmContenido);
            sbContenido.Append("];");

            #endregion

            #region Registro en Log

            if (!strDirectorioLog.EndsWith(@"\")) strDirectorioLog += @"\";

            DateTime dtInicio = DateTime.Now;

            while (true)
            {
                TimeSpan ts = DateTime.Now.Subtract(dtInicio);

                if (ts.Seconds >= 2) break;

                try
                {
                    File.AppendAllText(strDirectorioLog + strNombreLog, sbContenido.ToString(), Encoding.Unicode);
                    break;
                }
                catch (Exception ex)
                {
                    if (!(ex.Message.Contains("The process cannot access the file")
                        && ex.Message.Contains("because it is being used by another process")))
                    {
                        try
                        {
                            EventLog.WriteEntry("SIGC", "No se pudo registrar en el log el siguiente mensaje:\r\n" + sbContenido.ToString() +
                                                                "\r\n\r\nDebido al siguiente error:\r\n" + ex.Message + "\r\n\r\n" + ex.StackTrace);
                        }
                        catch { }
                        break;
                    }
                }
            }

            #endregion

            #region Si es ERROR o NOTIFICACION, se manda correo

            if (prmNivelMensajeLog == NivelMensajeLog.ERROR || prmNivelMensajeLog == NivelMensajeLog.NOTIFICACION)
            {
                try
                {
                    StringBuilder sbMensaje = new StringBuilder();
                    sbMensaje.AppendLine("El sistema SIGC ha enviado un correo con la siguiente información:");
                    sbMensaje.AppendLine("");
                    sbMensaje.Append("Tipo Mensaje: ");
                    sbMensaje.AppendLine(prmNivelMensajeLog.ToString("G"));
                    sbMensaje.Append("ID de Seguimiento en el log: ");
                    sbMensaje.AppendLine(prmIdSeguimiento);
                    sbMensaje.Append("Thread o Proceso: ");
                    sbMensaje.AppendLine(strThread);
                    sbMensaje.Append("Método: ");
                    sbMensaje.AppendLine(prmMetodo);
                    sbMensaje.Append("Ruta del log a revisar: ");
                    sbMensaje.AppendLine(strDirectorioLog + strNombreLog);
                    sbMensaje.AppendLine("Contenido de Mensaje:[");
                    sbMensaje.AppendLine(prmContenido);
                    sbMensaje.AppendLine("]");
                    sbMensaje.Append("*******************************************************************************************");

                    Log.Notificar(prmIdSeguimiento, prmNivelMensajeLog, sbMensaje.ToString());
                }
                catch { }
            }

            #endregion

        }

        /// <summary>
        /// Enviará un correo electrónico a los destinarios pasados como parámetros
        /// </summary>
        /// <param name="prmStrHashcode"></param>
        /// <param name="prmDe"></param>
        /// <param name="prmPara"></param>
        /// <param name="prmAsunto"></param>
        /// <param name="prmMensajeTexto"></param>
        public static void Notificar(String prmStrHashcode, String prmDe, String prmPara,
                                        String prmAsunto, String prmMensajeTexto)
        {
            Mailing oEnvioCorreo = new Mailing(prmStrHashcode);
            oEnvioCorreo.De = prmDe;
            oEnvioCorreo.Para = prmPara;
            oEnvioCorreo.Asunto = prmAsunto;
            oEnvioCorreo.ContenidoMensaje = prmMensajeTexto;
            oEnvioCorreo.EnviarCorreo();
        }

        /// <summary>
        /// Enviara un correo electronico a los destinarios configurados en el archivo de configuracion sección PARAMETROS PARA ENVIO DE CORREOS
        /// Esto dependerá del parámetro NivelMensaje que podría ser sólo ERROR o NOTIFICACION
        /// </summary>
        /// <param name="prmStrHashcode"></param>
        /// <param name="prmNivelMensaje"></param>
        /// <param name="prmMensajeTexto"></param>
        public static void Notificar(String prmStrHashcode, NivelMensajeLog prmNivelMensaje, String prmMensajeTexto)
        {
            String strDe = string.Empty;
            String strPara = string.Empty;
            String strAsunto = string.Empty;
            StackTrace stackTrace = new StackTrace();
            string strMetodosInvocados = string.Empty;

            if (prmNivelMensaje == NivelMensajeLog.NOTIFICACION)
            {
                for (int i = 0; i < stackTrace.FrameCount; i++)
                {
                    strMetodosInvocados += stackTrace.GetFrame(i).GetMethod().Name + "(" + stackTrace.GetFrame(i).GetFileLineNumber().ToString() + ") --> ";
                }
            }

            if (prmNivelMensaje == NivelMensajeLog.ERROR)
            {
                strDe = ConfigurationManager.AppSettings["MAIL_ERROR_DE"];
                strPara = ConfigurationManager.AppSettings["MAIL_ERROR_PARA"];
                strAsunto = ConfigurationManager.AppSettings["MAIL_ERROR_ASUNTO"];
            }
            else if (prmNivelMensaje == NivelMensajeLog.NOTIFICACION)
            {
                strDe = ConfigurationManager.AppSettings["MAIL_NOTIFICACION_DE"];
                strPara = ConfigurationManager.AppSettings["MAIL_NOTIFICACION_PARA"];
                strAsunto = ConfigurationManager.AppSettings["MAIL_NOTIFICACION_ASUNTO"];
                prmMensajeTexto = "StackTrace: " + strMetodosInvocados + "\r\n" + prmMensajeTexto;
            }

            if (prmNivelMensaje == NivelMensajeLog.ERROR || prmNivelMensaje == NivelMensajeLog.NOTIFICACION)
            {
                Log.Notificar(prmStrHashcode, strDe, strPara, strAsunto, prmMensajeTexto);
            }
        }
    }

    public enum TipoLog
    {
        Resumido = 0,
        Detallado = 1
    }

    public enum NivelMensajeLog
    {
        NINGUNO = 0,
        ERROR = 1,
        NOTIFICACION = 2
    }

    public enum ThreadSistema
    {
        APLICACIONSIGC = 1
    }
}
