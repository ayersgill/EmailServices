﻿using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Serilog;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using DASIT.EmailServices.Abstract;
using System.Net.Mail;
using System.Data;

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


                    object[] parameters = {
                        new SqlParameter("@param_profile_name", _profileName),
                        new SqlParameter("@param_recipients", recipientsString),
                        new SqlParameter("@param_subject", _subjectPrefix + mailMessage.Subject),
                        new SqlParameter("@param_body", tempBodyPrefix + mailMessage.Body),
                        new SqlParameter("@param_body_format", formatType),
                        new SqlParameter
                            {
                                ParameterName = "@retVal",
                                SqlDbType = SqlDbType.Int,
                                Direction = ParameterDirection.Output,
                                Value = -1
                            }
                    };

                    await db.Database.ExecuteSqlCommandAsync("EXEC @retVal = sp_send_dbmail @profile_name = @param_profile_name, " +
                        "@recipients = @param_recipients, @subject = @param_subject, @body = @param_body, @body_format = @param_body_format", parameters);


                    var result = (int) ((SqlParameter)parameters[5]).Value;

                    if(result != 0)
                    {

                        _logger.Error("Failed to send email, received result code of " + result);


                        throw new EmailSenderException("Failed to send email, received result code of " + result);


                    }

                }
            } catch (SqlException ex)
            {

                _logger.Error(ex, "Error Sending Email to Database Mail Server");

                throw new EmailSenderException("Failed to send email.");

            }

        }
    }
}