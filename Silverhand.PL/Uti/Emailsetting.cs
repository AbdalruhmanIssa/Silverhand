using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace Silverhand.PL.Uti
{
    public class Emailsetting:IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("aboody9068@gmail.com", "pzvp ysdc htga gkcd")
            };

            return client.SendMailAsync(
                new MailMessage(
                    from: "your.gmail@gmail.com",
                    to: email,
                    subject,
                    htmlMessage
                )
                { IsBodyHtml = true }
            );
        }
    }
}
