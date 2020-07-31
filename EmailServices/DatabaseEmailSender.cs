using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Serilog;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace EmailServices
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
            _profileName = _configuration["EmailServices:ProfileName"];


            _databaseMailContextOptions = new DbContextOptionsBuilder<EmailContext>()
                    .UseSqlServer(configuration["EmailServices:DatabaseEmailConnection"])
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
            _logger.Debug("Recipients {@0}, Subject {1}, Format Type {2}. Message {2}", recipients, subject, formatType, message);


            using (var db = new EmailContext(_databaseMailContextOptions))
            {
                string recipientsString = "";

                foreach(string recipient in recipients)
                {
                    recipientsString += recipient + ";";
                }

                /*
              db.Database.ExecuteSqlCommand("EXEC sp_send_dbmail @profile_name, @recipients, @subject, @body_format, @body",  
                    new SqlParameter("@profile_name", _profileName),
                    new SqlParameter("@recipients", recipientsString),
                    new SqlParameter("@subject", subject),
                    new SqlParameter("@body_format", formatType),
                    new SqlParameter("@body", message)
                );
                */

                db.Database.ExecuteSqlCommand("EXEC sp_send_dbmail @profile_name = {0} , @recipients = {1}, @subject = {2}, @body = {3}, @body_format = {4}", _profileName, recipientsString, subject, message, formatType);

                //db.Database.ExecuteSqlCommand("EXEC sp_send_dbmail @profile_name = 'Demo_Profile' , @recipients = 'matthew.olson@oregon.gov; larry.mccartin@oregon.gov', @subject = 'testning', @body = 'testing 04C dbmail'");



            }

        }
    }
}