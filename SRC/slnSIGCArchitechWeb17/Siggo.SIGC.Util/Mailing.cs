using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Siggo.SIGC.Util
{
    public class Mailing
    {
        private string strHashcode = "";
        public string De = null;
        public string Para = null;
        public string Asunto = null;
        public string ContenidoMensaje = null;

        public Mailing() { }

        public Mailing(string prmHashcode)
        {
            strHashcode = prmHashcode;
        }

        public Mailing(string prmDe, string prmPara, string prmAsunto, string prmContenidoMensaje)
        {
            De = prmDe;
            Para = prmPara;
            Asunto = prmAsunto;
            ContenidoMensaje = prmContenidoMensaje;
        }

        public void EnviarCorreo()
        {
            try
            {
                MailMessage oMessage = new MailMessage(De, Para);

                oMessage.Subject = Asunto;
                oMessage.Body = ContenidoMensaje;
                oMessage.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient();
                if (!String.IsNullOrEmpty(smtp.Host))
                {
                    smtp.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);
                    smtp.SendAsync(oMessage, null);
                }
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, strHashcode,
                    "Mailing.EnviarCorreo", "No se pudo enviar el email. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
            }
        }

        private void smtp_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, strHashcode,
                    "Mailing.smtp_SendCompleted", "Error al intentar enviar la notificacion: " + e.Error.Message, NivelMensajeLog.NINGUNO);
            }
        }
    }
}
