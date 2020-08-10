using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Serilog;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Net.Http;
using Flurl.Http;

namespace DASIT.EmailServices
{
    public class EmailAPISender : IEmailService
    {
        private readonly IConfiguration _configuration;

        private HttpClient client;
        private string _url { get; set; }
    private string _token { get; set; }
    private string _fromAddress { get; set; }

        private readonly ILogger _logger;

        public EmailAPISender(IConfiguration configuration)
        {
            _configuration = configuration;

            _url = configuration["EmailServices:EmailAPISender:APISendUrl"];
            _token = configuration["EmailServices:EmailAPISender:SecurityToken"];
            _fromAddress = configuration["EmailServices:EmailAPISender:FromAddress"];

            _logger = Log.ForContext<EmailAPISender>();

            _logger.Debug("Using mail api url {0} with token {1}", _url, _token);

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