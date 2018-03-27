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
            //try
            //{
            //FormsAuthenticationTicket formsTicket = FormsAuthentication.Decrypt(token);
            //FormsAuthenticationTicket formsTicket = FormsAuthentication.Decrypt("917DF331A525E270774196CAFFC823641D173FB740CCB6ADD35C8B806EEAE5B986ED2A635DF21C95E3CC21D77FEDA416AEF36B4FE19ACDCD6161C5CB2F30BEBA0E4E324B8B1B70E9C9587F0F4A259D30E2AF61804744A64C12EBE60366AF144A1B04191E89D96EC91BCB7414B5F3CB9DAF219CB3FF11DC14EBA5FB40CB080FC5FC0F9A0D9246458E61C5A13C748D45603E6F3553451EF288AD732F363B6B2C0D6A1A8A4AA6B6E9867DD9A28415B8C11FBA5E8718910067D14B75CAB155998E096659645ACE2693EAD0B7E1A7C90AE812D64392E48D3204166799D1A9934BF05FB82BD1AAD5C010E7EDA389F3596A54CBCF1C883F7D9C7735FA7D2AE91AC5DFAB");

            //string a = "917DF331A525E270774196CAFFC823641D173FB740CCB6ADD35C8B806EEAE5B986ED2A635DF21C95E3CC21D77FEDA416AEF36B4FE19ACDCD6161C5CB2F30BEBA0E4E324B8B1B70E9C9587F0F4A259D30E2AF61804744A64C12EBE60366AF144A1B04191E89D96EC91BCB7414B5F3CB9DAF219CB3FF11DC14EBA5FB40CB080FC5FC0F9A0D9246458E61C5A13C748D45603E6F3553451EF288AD732F363B6B2C0D6A1A8A4AA6B6E9867DD9A28415B8C11FBA5E8718910067D14B75CAB155998E096659645ACE2693EAD0B7E1A7C90AE812D64392E48D3204166799D1A9934BF05FB82BD1AAD5C010E7EDA389F3596A54CBCF1C883F7D9C7735FA7D2AE91AC5DFAB";

            //string b = "EA08D9C806E1B24FDD8FAB72B5AF843C0176A8CA51923C0429BED541EF3F6B82B22DA83D9FF41E9E42F88915681646C160F611F5E4CDC20B117DF86073BCC3020B3FA758DD5EDF07DE0B910136C53036954F5D697132A3D6126D5676C2343E7948EC1D8C1FF0272AD7715DCDC1F7333969D61D02D636FDC8AC3AEECF08FD02B311483FC61F9044A5601037727A752294194CDB5959B0B4849570244A378D1A163B16730B3B87185CD7980FA83F4D42A75C831ACB8DD0D4D44C88705E92A0E5B4C1B3F7EFE1EE627BB2632866403E7B765893845C463AD8B46A50FEB2D4B7EA2CD75FDAF738D0F5E555F0EB5EE030A2EEFB00E5D4FFD62C4D4160614EEFBB49762D7FD255BDA0BDB8A3398906E46CC484E7462D6734689A3F4343A86BFEDFA85F";



                //string[] userData = formsTicket.UserData.Split(new string[] { Delimiter }, StringSplitOptions.None);

                //Animate.Persona p = Animate.Persona.Get(Guid.Parse(formsTicket.Name.ToString()));
                //Animate.Membresia.Usuario u = Animate.Membresia.Usuario.Get(p.RunCuerpo, p.RunDigito);
                return true;
            //}
            //catch
            //{
            //    return false;
            //}
        }
    }
}