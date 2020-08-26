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





        public override async Task SendEmailAsync(MailMessage mailMessage)
        {

            _logger.Information("SendEmailAsync Called");
            _logger.Debug("Mail Message {@0}", mailMessage);

            string tempBodyPrefix;

            var msg = new MimeMessage();

            if(mailMessage.From != null)
            {
                msg.From.Add(new MailboxAddress(mailMessage.From.DisplayName, mailMessage.From.Address));
            } else
            {
                msg.From.Add(new MailboxAddress(_fromName, _fromAddress));
            }

            

            msg.Subject = _subjectPrefix + mailMessage.Subject;

            foreach (var email in mailMessage.To)
            {
                msg.To.Add(MailboxAddress.Parse(email.Address));
            }

            if (mailMessage.IsBodyHtml)
            {
                tempBodyPrefix = _bodyPrefix.Replace("\n", "<br>");
                msg.Body = new TextPart(TextFormat.Html) { Text = tempBodyPrefix + mailMessage.Body };
            } else {
                tempBodyPrefix = _bodyPrefix.Replace("<br>", "\n");
                msg.Body = new TextPart(TextFormat.Plain) { Text = tempBodyPrefix + mailMessage.Body };
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