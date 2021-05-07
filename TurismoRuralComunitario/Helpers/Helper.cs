using System;
using System.IO;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TurismoRuralComunitario.Helpers
{
    public static class Helper
    {
        public static string CORREOWEB = "vasborsas@gmail.com";
        public static string PASSWORD = "vasbor12345";
        public static void enviarCorreo(String correoDestino, String userToken, String mensaje)
        {
            try
            {
                var Emailtemplate = new StreamReader(AppDomain.CurrentDomain.BaseDirectory.Insert
                    (AppDomain.CurrentDomain.BaseDirectory.Length, "Recursos\\SendMail\\CambiarPassword.html"));
                var strBody = string.Format(Emailtemplate.ReadToEnd(), userToken);
                Emailtemplate.Close();
                Emailtemplate.Dispose();
                Emailtemplate = null;
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                //Especificamos el correo desde el que se enviará el Email y el nombre de la persona que lo envía
                mail.From = new MailAddress("vasborsas@gmail.com", "Turismo Rural");
                SmtpServer.Host = "smtp.gmail.com";
                //Aquí ponemos el asunto del correo
                mail.Subject = mensaje;
                var URL = "Cuenta/CambiarPassword?token=";
                //Aquí ponemos el mensaje que incluirá el correo

                strBody = strBody.Replace("token", SSLyHost() + URL + userToken);
                mail.Body = strBody;
                //mail.Body = "Por favor ingrese al siguiente link para recuperar su contraseña";
                //Especificamos a quien enviaremos el Email, no es necesario que sea Gmail, puede ser cualquier otro proveedor
                mail.To.Add(correoDestino);
                //Si queremos enviar archivos adjuntos tenemos que especificar la ruta en donde se encuentran
                //mail.Attachments.Add(new Attachment(@"C:\Documentos\carta.docx"));
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;
                //Configuracion del SMTP
                SmtpServer.Port = 587; //Puerto que utiliza Gmail para sus servicios
                                       //Especificamos las credenciales con las que enviaremos el mail
                SmtpServer.Credentials = new System.Net.NetworkCredential(CORREOWEB, PASSWORD);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                mail.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string Encriptar(string input)
        {
            SHA512CryptoServiceProvider provider = new SHA512CryptoServiceProvider();
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashedBytes = provider.ComputeHash(inputBytes);
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < hashedBytes.Length; i++)
                output.Append(hashedBytes[i].ToString("x2").ToLower());
            return output.ToString();
        }
        public static string SSLyHost()
        {
            var SSLHOST = new Uri(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority));
            return SSLHOST.ToString();
        }
    }
}