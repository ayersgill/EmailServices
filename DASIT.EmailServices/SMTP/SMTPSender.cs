using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Serilog;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using MimeKit;
using MimeKit.Text;
using System.Net.Mail;
using DASIT.EmailServices.AspNet;

namespace DASIT.EmailServices.SMTP
{
    public class SMTPSender : IEmailService
    {
        private readonly IConfiguration _configuration;

        private string _fromAddress { get; set; }

        private string _fromName { get; set; }

        private string _server { get; set; }

        private int _port { get; set; }


        private readonly ILogger _logger;

        public SMTPSender(IConfiguration configuration)
        {
            _configuration = configuration;
            _fromAddress = _configuration["EmailServices:MailKitSender:FromAddress"];
            _fromName = _configuration["EmailServices:MailKitSender:FromName"];
            _server = _configuration["EmailServices:MailKitSender:Server"];
            _port = int.Parse(_configuration["EmailServices:MailKitSender:Port"]);


            _logger = Log.ForContext<SMTPSender>();

            _logger.Debug("Sending from {0} at {1} using {2}:{3}", _fromName, _fromAddress, _server, _port);

        }

      


        public async Task SendHtmlEmailAsync(string recipient, string subject, string htmlMessage)
        {
            _logger.Information("SendHtmlEmailAsync Called");
            _logger.Debug("Email {0}, Subject {1}, HtmlMessage {2}", recipient, subject, htmlMessage);

            await SendEmailAsync(new string[] { recipient }, subject, "HTML", htmlMessage);
        }

        public async Task SendHtmlEmailAsync(string[] recipients, string subject, string htmlMessage)
        {
            _logger.Information("SendHtmlEmailAsync Called");
            _logger.Debug("Recipients {@0}, Subject {1}, HtmlMessage {2}", recipients, subject, htmlMessage);

            await SendEmailAsync(recipients, subject, "HTML", htmlMessage);
        }

        public async Task SendEmailAsync(string recipient, string subject, string textMessage)
        {
            _logger.Information("SendTextEmailAsync Called");
            _logger.Debug("Email {0}, Subject {1}, TextMessage {2}", recipient, subject, textMessage);

            await SendEmailAsync(new string[] { recipient }, subject, "TEXT", textMessage);
        }

        public async Task SendEmailAsync(string[] recipients, string subject, string textMessage)
        {
            _logger.Information("SendTextEmailAsync Called");
            _logger.Debug("Recipients {@0}, Subject {1}, TextMessage {2}", recipients, subject, textMessage);

            await SendEmailAsync(recipients, subject, "TEXT", textMessage);
        }

        private async Task SendEmailAsync(string[] recipients, string subject, string formatType, string message)
        {

            _logger.Information("SendEmailAsync Called");
            _logger.Debug("Recipients {@0}, Subject {1}, Format Type {2}. Message {3}", recipients, subject, formatType, message);

            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress(_fromName, _fromAddress));

            msg.Subject = subject;

            foreach (var email in recipients)
            {
                msg.To.Add(MailboxAddress.Parse(email));
            }


            if(formatType == "TEXT")
            {
                msg.Body = new TextPart(TextFormat.Plain) { Text = message };

            } else
            {
                msg.Body = new TextPart(TextFormat.Html) { Text = message };
            }


            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {

                    await client.ConnectAsync(_server, _port, false);
                    await client.SendAsync(msg);
                    await client.DisconnectAsync(true);
                }
                catch (SocketException ex)
                {
                    _logger.Error(ex, "Error Sending Email to SMTP Server");

                    throw new EmailSenderException("Failed to send email.");
                }
            }

        }
    }
}