using System.Threading.Tasks;
using DASIT.EmailServices.Abstract;
using System.Net.Mail;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace DASIT.EmailServices.DatabaseMail
{
    public abstract class DatabaseEmailSenderAbstract : EmailSenderAbstract
    {
       
        protected string _profileName { get; set; }

        protected string _databaseConnectionString { get; set; }

        public override async Task SendEmailAsync(MailMessage mailMessage)
        {
            _logger.Debug("SendEmailAsync {mailMessage}", JsonConvert.SerializeObject(mailMessage));

            string formatType;
            string tempBodyPrefix;

            try
            {

                using (SqlConnection conn = new SqlConnection(_databaseConnectionString))
                {

                    conn.Open();

                    SqlCommand command = conn.CreateCommand();

                    command.CommandText = "sp_send_dbmail";
                    command.CommandType = CommandType.StoredProcedure;

                    string recipientsString = "";

                    foreach (var recipient in mailMessage.To)
                    {
                        recipientsString += recipient.Address + ";";
                    }

                    _logger.Debug("Sending to recipients string {recipientString}", recipientsString);

                    if(mailMessage.IsBodyHtml)
                    {
                        formatType = "HTML";
                        tempBodyPrefix = _bodyPrefix.Replace("\n", "<br>");
                        
                    } else
                    {
                        formatType = "TEXT";
                        tempBodyPrefix = _bodyPrefix.Replace("<br>", "\n");
                    }

                    command.Parameters.Add(new SqlParameter("@profile_name", _profileName));
                    command.Parameters.Add(new SqlParameter("@recipients", recipientsString));
                    command.Parameters.Add(new SqlParameter("@subject", _subjectPrefix + mailMessage.Subject));
                    command.Parameters.Add(new SqlParameter("@body", tempBodyPrefix + mailMessage.Body));
                    command.Parameters.Add(new SqlParameter("@body_format", formatType));

                    var retVal = new SqlParameter
                    {
                        ParameterName = "OUTPUT",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.ReturnValue,
                        Value = -1
                    };



                    //add the parameter to the SqlCommand object
                    command.Parameters.Add(retVal);

                    _logger.Debug("Calling stored procedure with {@sqlParameters}", command.Parameters);

                    await command.ExecuteNonQueryAsync();

                    var result = (int) retVal.Value;

                    if(result != 0)
                    {

                        _logger.Error("Failed to send email, received result code {result_code}", result);


                        throw new EmailSenderException("Failed to send email, received result code of " + result);


                    }

                }
            } catch (SqlException ex)
            {

                _logger.Error(ex, "Error Sending Email to Database Mail Server");

                throw new EmailSenderException("Failed to send email.", ex);

            }

        }
    }
}