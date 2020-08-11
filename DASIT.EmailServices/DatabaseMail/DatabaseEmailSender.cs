using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Serilog;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using DASIT.EmailServices.Interface;
using DASIT.EmailServices.AspNet;

namespace DASIT.EmailServices.DatabaseMail
{
    public class DatabaseEmailSender : IEmailService
    {
        private readonly IConfiguration _configuration;

        private string _profileName { get; set; }

        private DbContextOptions<EmailContext> _databaseMailContextOptions { get; set; }

        private readonly ILogger _logger;

        public DatabaseEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
            _profileName = _configuration["EmailServices:DatabaseEmailSender:ProfileName"];


            _databaseMailContextOptions = new DbContextOptionsBuilder<EmailContext>()
                    .UseSqlServer(configuration["EmailServices:DatabaseEmailSender:DatabaseEmailConnection"])
                    .Options;

            _logger = Log.ForContext<DatabaseEmailSender>();

            _logger.Debug("Using mail server profile {0}", _profileName);

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

            try
            {

                using (var db = new EmailContext(_databaseMailContextOptions))
                {
                    string recipientsString = "";

                    foreach (string recipient in recipients)
                    {
                        recipientsString += recipient + ";";
                    }

                    _logger.Debug("Sending to recipients string {0}", recipientsString);

                    await db.Database.ExecuteSqlCommandAsync("EXEC sp_send_dbmail @profile_name = {0} , " +
                        "@recipients = {1}, " +
                        "@subject = {2}, " +
                        "@body = {3}, " +
                        "@body_format = {4}",
                        _profileName,
                        recipientsString,
                        subject,
                        message,
                        formatType);

                }
            } catch (SqlException ex)
            {

                _logger.Error(ex, "Error Sending Email to Database Mail Server");

                throw new EmailSenderException("Failed to send email.");

            }

        }
    }
}