using MailKit.Net.Smtp;
using MimeKit;
using HipercorWeb.Interfaces;
using Microsoft.Extensions.Configuration;

namespace HipercorWeb.Services
{
    public class MailKitService : ISendEmail
    {
        public void Send(string to, string subject, string body)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();

            MimeMessage mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("HipercorWeb", config["Smtp:Email"]));
            mailMessage.To.Add(new MailboxAddress("Querido Cliente", to));
            mailMessage.Subject = subject;
            mailMessage.Body = new BodyBuilder { HtmlBody = body }.ToMessageBody();

            using (SmtpClient smtpClient = new SmtpClient())
            {
                smtpClient.Connect(config["Smtp:Host"], int.Parse(config["Smtp:Port"]), true);
                smtpClient.Authenticate(config["Smtp:Email"], config["Smtp:Password"]);
                smtpClient.Send(mailMessage);
                smtpClient.Disconnect(true);
            }
        }
    }
}
