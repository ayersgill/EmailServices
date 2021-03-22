using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Serilog;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using MimeKit;
using MimeKit.Text;
using System.Net.Mail;
using DASIT.EmailServices.Abstract;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using DASIT.EmailServices.AspNetCore;

namespace DASIT.EmailServices.SMTP
{
    public abstract class SMTPSenderAbstract : EmailSenderAbstract
    {


        protected string _fromAddress { get; set; }

        protected string _fromName { get; set; }

        protected string _server { get; set; }

        protected int _port { get; set; }


        public override async Task SendEmailAsync(MailMessage mailMessage)
        {

        /*    JsonConvert.DefaultSettings = (() =>
            {
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new MailAddressConverter());
                settings.Converters.Add(new MemoryStreamConverter());
                return settings;
            });*/


            // Yes, I hate having to do this, by MailMessage is non-serializable, so we need to do this stupid hack
            _logger.Debug("SendEmailAsync Called with MailMessage {message}", JsonConvert.SerializeObject(mailMessage));

            string tempBodyPrefix;

            var msg = new MimeMessage();

            if(mailMessage.From != null)
            {
                _logger.Debug("Creating email from {displayName} at {address}", mailMessage.From.DisplayName, mailMessage.From.Address);
                msg.From.Add(new MailboxAddress(mailMessage.From.DisplayName, mailMessage.From.Address));
            } else
            {
                _logger.Debug("Creating email from {displayName} at {address}", _fromName, _fromAddress);
                msg.From.Add(new MailboxAddress(_fromName, _fromAddress));
            }

            msg.Subject = _subjectPrefix + mailMessage.Subject;

            _logger.Debug("Creating email with subject {subject}", msg.Subject);

            foreach (var email in mailMessage.To)
            {
                _logger.Debug("Adding {email} as email recipient", email.Address);
                msg.To.Add(MailboxAddress.Parse(email.Address));
            }

            if (mailMessage.IsBodyHtml)
            {
                tempBodyPrefix = _bodyPrefix.Replace("\n", "<br \\>");
                msg.Body = new TextPart(TextFormat.Html) { Text = tempBodyPrefix + mailMessage.Body };
                _logger.Debug("HTML Body for Email {body}", msg.Body);
            } else {
                tempBodyPrefix = _bodyPrefix.Replace("<br>", "\n");
                msg.Body = new TextPart(TextFormat.Plain) { Text = tempBodyPrefix + mailMessage.Body };
                _logger.Debug("Text Body for Email {body}", msg.Body);
            }

           

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    _logger.Debug("Connecting to {server}:{port}", _server, _port);
                    await client.ConnectAsync(_server, _port, false);

    

                    _logger.Debug("Sending Message {mimeMessage}", JsonConvert.SerializeObject(msg));
                    await client.SendAsync(msg);
                    await client.DisconnectAsync(true);
                }
                catch (SocketException ex)
                {
                    _logger.Error(ex, "Error Sending Email to SMTP Server");

                    throw new EmailSenderException("Failed to send email.", ex);
                }
            }

        }
    }
}