using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Serilog;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace DASIT.EmailServices
{
    public class TrashEmailSender : IEmailService
    {


        private readonly ILogger _logger;

        public TrashEmailSender(IConfiguration configuration)
        {


            _logger = Log.ForContext<DatabaseEmailSender>();


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
            _logger.Information("SendEmailAsync Called");
            _logger.Debug("Email {0}, Subject {1}, TextMessage {2}", recipient, subject, textMessage);

            await SendEmailAsync(new string[] { recipient }, subject, "TEXT", textMessage);
        }

        public async Task SendEmailAsync(string[] recipients, string subject, string textMessage)
        {
            _logger.Information("SendEmailAsync Called");
            _logger.Debug("Recipients {@0}, Subject {1}, TextMessage {2}", recipients, subject, textMessage);

            await SendEmailAsync(recipients, subject, "TEXT", textMessage);
        }

        private async Task SendEmailAsync(string[] recipients, string subject, string formatType, string message)
        {

            _logger.Information("SendEmailAsync Called");
            _logger.Debug("Recipients {@0}, Subject {1}, Format Type {2}. Message {2}", recipients, subject, formatType, message);


            _logger.Warning("All emails will be sent to trash.");
        }
    }
}