using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Security;

namespace RecursoCurricular
{
    public static class Helper
    {
        const string Delimiter = ",@";

        public static void SendMail(string to, string subject, string body)
        {
            MailMessage message = new MailMessage();

            message.From = new MailAddress("applicationserver@icastellano.cl");
            message.To.Add(to);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = false;
            message.Priority = MailPriority.Normal;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("applicationserver@icastellano.cl", "insignia", "");
            smtp.Send(message);
        }

        public static bool ValidaRun(int runCuerpo, char runDigito)
        {
            return RecursoCurricular.Helper.GetDigito(runCuerpo).ToString().ToLower().Equals(runDigito.ToString().ToLower());
        }

        private static char GetDigito(int cuerpo)
        {
            int contador = 0;
            int multiplo = 2;

            while (cuerpo != 0)
            {
                contador += (cuerpo % 10) * multiplo;

                cuerpo /= 10;

                multiplo++;

                if (multiplo == 8) multiplo = 2;
            }

            contador = 11 - (contador % 11);

            if (contador == 10) return 'K';
            else if (contador == 11) return '0';
            else return char.Parse(contador.ToString());
        }

        public static string GenerateToken(string run, Guid personaId)
        {
            string[] userData = new string[2];

            userData[0] = personaId.ToString();
            userData[1] = run;

            FormsAuthenticationTicket formsTicket = new FormsAuthenticationTicket(1, personaId.ToString(), DateTime.Now, DateTime.Now.AddMinutes(500), true, string.Join(Delimiter, userData));

            string encryptedTicket = FormsAuthentication.Encrypt(formsTicket);

            return encryptedTicket;
        }

        public static bool ValidateToken(string token)
        {
            try
            {
                FormsAuthenticationTicket formsTicket = FormsAuthentication.Decrypt(token);

                string[] userData = formsTicket.UserData.Split(new string[] { Delimiter }, StringSplitOptions.None);

                //Animate.Persona p = Animate.Persona.Get(Guid.Parse(formsTicket.Name.ToString()));
                //Animate.Membresia.Usuario u = Animate.Membresia.Usuario.Get(p.RunCuerpo, p.RunDigito);

                //if (!u.Bloqueado && u.HabilitadoJuego == true)
                //{
                return true;
                //}
                //else
                //{
                //    return false;
                //}
            }
            catch
            {
                return false;
            }
        }
    }
}