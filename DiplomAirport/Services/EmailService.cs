using Microsoft.Extensions.Logging;
using MimeKit;
using System;

namespace DiplomAirport.Helpers
{
    public class SmtpGoogleServer
    {
        public readonly string host = "smtp.gmail.com";
        public readonly int port = 465;
        public readonly bool useSSL = true;
        public readonly string login = "spn337test@gmail.com";
        public readonly string password = "123qweR$";
    }
    public class EmailService
    {
        private readonly SmtpGoogleServer _smtp;

        private readonly ILogger<EmailService> _logger;
        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
            _smtp = new SmtpGoogleServer();
        }
        public void SendEmail(string email, string subject, string message)
        {
            try
            {
                //
                //налаштувати google smtp-server та відправити лист
                //
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("ToyStore", _smtp.login));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = message
                };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect(_smtp.host, _smtp.port, _smtp.useSSL);
                    client.Authenticate(_smtp.login, _smtp.password);
                    client.Send(emailMessage);
                    client.Disconnect(true);

                    _logger.LogInformation("Send message will be success");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}
