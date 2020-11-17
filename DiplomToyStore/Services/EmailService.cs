using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;

namespace DiplomToyStore.Helpers
{
    public class SmtpGoogleServer
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSSL { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
    public class EmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly IConfiguration _configuration;

        public EmailService(ILogger<EmailService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public void SendEmail(string email, string subject, string message)
        {
            try
            {
                var smtp = new SmtpGoogleServer()
                {
                    Host = _configuration["Data:SmtpGoogleServer:Host"],
                    Port = Convert.ToInt32(_configuration["Data:SmtpGoogleServer:Port"]),
                    UseSSL = Convert.ToBoolean(_configuration["Data:SmtpGoogleServer:UseSSL"]),
                    Login = _configuration["Data:SmtpGoogleServer:Login"],
                    Password = _configuration["Data:SmtpGoogleServer:Password"]
                };

                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("ToyStore", smtp.Login));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = message
                };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect(smtp.Host, smtp.Port, smtp.UseSSL);
                    client.Authenticate(smtp.Login, smtp.Password);
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
