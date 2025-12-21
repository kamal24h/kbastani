using System.Net;
using System.Net.Mail;

namespace WebApp.Services
{           
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var host = _config["EmailSettings:Host"];
            var port = int.Parse(_config["EmailSettings:Port"]);
            var username = _config["EmailSettings:UserName"];
            var password = _config["EmailSettings:Password"];
            var enableSSL = bool.Parse(_config["EmailSettings:EnableSSL"]);

            var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(username, password),
                EnableSsl = enableSSL
            };

            var mail = new MailMessage(username, to, subject, body)
            {
                IsBodyHtml = true
            };

            await client.SendMailAsync(mail);
        }
    }

}
