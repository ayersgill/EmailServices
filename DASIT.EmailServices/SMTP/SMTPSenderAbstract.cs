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

namespace DASIT.EmailServices.SMTP
{
    public abstract class SMTPSenderAbstract : EmailSenderAbstract
    {
       

        protected string _fromAddress { get; set; }

        protected string _fromName { get; set; }

        protected string _server { get; set; }

        protected int _port { get; set; }





        public override async Task SendEmailAsync(string[] recipients, string subject, string formatType, string message)
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