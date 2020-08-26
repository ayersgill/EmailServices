using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Serilog;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using DASIT.EmailServices.Abstract;
using System.Net.Mail;

namespace DASIT.EmailServices.DatabaseMail
{
    public abstract class DatabaseEmailSenderAbstract : EmailSenderAbstract
    {
       
        protected string _profileName { get; set; }

        protected DbContextOptions<EmailContext> _databaseMailContextOptions { get; set; }

        public override async Task SendEmailAsync(MailMessage mailMessage)
        {

            _logger.Information("SendEmailAsync Called");
            _logger.Debug("Mail Message {@0}", mailMessage);

            string formatType;
            string tempBodyPrefix;

            try
            {

                using (var db = new EmailContext(_databaseMailContextOptions))
                {
                    string recipientsString = "";

                    foreach (var recipient in mailMessage.To)
                    {
                        recipientsString += recipient.Address + ";";
                    }

                    _logger.Debug("Sending to recipients string {0}", recipientsString);

                    if(mailMessage.IsBodyHtml)
                    {
                        formatType = "HTML";
                        tempBodyPrefix = _bodyPrefix.Replace("\n", "<br>");
                        
                    } else
                    {
                        formatType = "TEXT";
                        tempBodyPrefix = _bodyPrefix.Replace("<br>", "\n");
                    }

                    await db.Database.ExecuteSqlCommandAsync("EXEC sp_send_dbmail @profile_name = {0} , " +
                        "@recipients = {1}, " +
                        "@subject = {2}, " +
                        "@body = {3}, " +
                        "@body_format = {4}",
                        _profileName,
                        recipientsString,
                        _subjectPrefix + mailMessage.Subject,
                        tempBodyPrefix + mailMessage.Body,
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