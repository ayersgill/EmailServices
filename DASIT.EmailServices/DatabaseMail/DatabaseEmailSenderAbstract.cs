using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Serilog;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using DASIT.EmailServices.Abstract;

namespace DASIT.EmailServices.DatabaseMail
{
    public abstract class DatabaseEmailSenderAbstract : EmailSenderAbstract
    {
       
        protected string _profileName { get; set; }

        protected DbContextOptions<EmailContext> _databaseMailContextOptions { get; set; }

        public override async Task SendEmailAsync(string[] recipients, string subject, string formatType, string message)
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