using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace Silverhand.PL.Uti
{
    public class Emailsetting:IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)//port number
            {
                EnableSsl = true, //encreption
                UseDefaultCredentials = false,//custom credintials
                Credentials = new NetworkCredential("aboody9068@gmail.com", "pzvp ysdc htga gkcd")//app password
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
