using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Serilog;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Net.Http;
using Flurl.Http;
using DASIT.EmailServices.Abstract;

namespace DASIT.EmailServices.EmailAPI
{
    public abstract class EmailAPISenderAbstract : EmailSenderAbstract
    {
       
        protected string _url { get; set; }
        protected string _token { get; set; }
        protected string _fromAddress { get; set; }

       

        public override async Task SendEmailAsync(string[] recipients, string subject, string formatType, string message)
        {

            _logger.Information("SendEmailAsync Called");
            _logger.Debug("Recipients {@0}, Subject {1}, Format Type {2}. Message {2}", recipients, subject, formatType, message);

            var emailMessage = new EmailMessage
            {
                From = _fromAddress,
                To = recipients,
                Subject = subject,
                Body = message

            };

            bool result = false;

            try
            {

                result = await (_url)
                   .WithHeader("X-Api-Key", _token)
                   .PostJsonAsync(emailMessage)
                   .ReceiveJson<bool>();

            } catch (FlurlHttpException ex)
            {
                _logger.Error("HTTP Error", ex);
            }

            if(!result)
            {
                _logger.Error("Error Sending Email to Email API");

                throw new EmailSenderException("Failed to send email.");
            }

        }
    }
}