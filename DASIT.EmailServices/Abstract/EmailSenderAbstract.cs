using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Serilog;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Net.Http;
using Flurl.Http;
using System.Net.Mail;

namespace DASIT.EmailServices.Abstract
{
    public abstract class EmailSenderAbstract
    {

        protected ILogger _logger;


        public async Task SendHtmlEmailAsync(string recipient, string subject, string htmlMessage)
        {
            _logger.Information("SendHtmlEmailAsync Called");
            _logger.Debug("Email {0}, Subject {1}, HtmlMessage {2}", recipient, subject, htmlMessage);


            MailMessage email = new MailMessage()
            {
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            email.To.Add(recipient);

            await SendEmailAsync(email);
        }

        public async Task SendHtmlEmailAsync(string[] recipients, string subject, string htmlMessage)
        {
            _logger.Information("SendHtmlEmailAsync Called");
            _logger.Debug("Recipients {@0}, Subject {1}, HtmlMessage {2}", recipients, subject, htmlMessage);

            MailMessage email = new MailMessage()
            {
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };


            foreach (string recipient in recipients)
            {
                email.To.Add(recipient);
            }

            await SendEmailAsync(email);
        }

        public async Task SendEmailAsync(string recipient, string subject, string textMessage)
        {
            _logger.Information("SendEmailAsync Called");
            _logger.Debug("Email {0}, Subject {1}, TextMessage {2}", recipient, subject, textMessage);

            MailMessage email = new MailMessage()
            {
                Subject = subject,
                Body = textMessage,
                IsBodyHtml = false
            };

            email.To.Add(recipient);

            await SendEmailAsync(email);

        }

        public async Task SendEmailAsync(string[] recipients, string subject, string textMessage)
        {
            _logger.Information("SendEmailAsync Called");
            _logger.Debug("Recipients {@0}, Subject {1}, TextMessage {2}", recipients, subject, textMessage);

            MailMessage email = new MailMessage()
            {
                Subject = subject,
                Body = textMessage,
                IsBodyHtml = false
            };


            foreach (string recipient in recipients)
            {
                email.To.Add(recipient);
            }

            await SendEmailAsync(email);

        }

        public string[] GetArrayOfToAddresses(MailMessage mailMessage)
        {
            string[] emailAddressArray = new string[mailMessage.To.Count];

            int counter = 0;
            foreach (var recipient in mailMessage.To)
            {
                emailAddressArray[counter] = recipient.Address;
                counter++;
            }

            return emailAddressArray;
        }

       // public abstract Task SendEmailAsync(string[] recipients, string subject, string formatType, string message);

        public abstract Task SendEmailAsync(MailMessage mailMessage);


    }
}