using Microsoft.Extensions.Options;
using PhotoSharinggApplication.Models;
using System.Net.Mail;

namespace PhotoSharinggApplication.Services
{
    public class EmailService
    {
        private readonly IOptions<Email> _dariJson;
        public EmailService(IOptions<Email> em)
        {
            _dariJson = em;
        }

        public bool KirimEmail (string tujuan, string judulEmail, string isiEmail)
        {
            try
            {

                Email e = new Email();
                e.NamaClient = _dariJson.Value.NamaClient;
                e.Port = _dariJson.Value.Port;
                e.EmailKita = _dariJson.Value.EmailKita;
                e.PasswordEmail = _dariJson.Value.PasswordEmail;

                MailMessage m = new MailMessage();
                m.From = new MailAddress(e.EmailKita);
                m.Subject = judulEmail;
                m.Body = isiEmail;
                m.IsBodyHtml = true;
                m.To.Add(tujuan);

                SmtpClient s = new SmtpClient(e.NamaClient);
                s.Port = e.Port;
                s.Credentials = new System.Net.NetworkCredential(e.EmailKita, e.PasswordEmail);
                s.EnableSsl = true;
                s.Send(m);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
