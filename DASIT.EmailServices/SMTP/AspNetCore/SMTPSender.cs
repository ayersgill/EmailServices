using Microsoft.Extensions.Configuration;
using Serilog;
using DASIT.EmailServices.AspNetCore;
using System.Text.Json;

namespace DASIT.EmailServices.SMTP.AspNetCore
{
    public class SMTPSender : SMTPSenderAbstract, IEmailService
    {
       

        public SMTPSender(IConfiguration configuration)
        {
           
            _fromAddress = configuration["EmailServices:MailKitSender:FromAddress"] ?? throw new EmailSenderException("MailKitSender FromAddress set to null");
            _fromName = configuration["EmailServices:MailKitSender:FromName"] ?? throw new EmailSenderException("MailKitSender FromName set to null");
            _server = configuration["EmailServices:MailKitSender:Server"] ?? throw new EmailSenderException("MailKitSender Server set to null");

            var tempPort = configuration["EmailServices:MailKitSender:Port"] ?? throw new EmailSenderException("MailKitSender Port set to null");

            _port = int.Parse(tempPort);

            _subjectPrefix = configuration["EmailServices:SubjectPrefix"] ?? "";
            _bodyPrefix = configuration["EmailServices:BodyPrefix"] ?? "";

            _mailMessageSerializerOptions = new JsonSerializerOptions
                  {
                      Converters =
                            {
                                new MailMessageJsonConverter()
                            }
                  };

            _logger = Log.ForContext<SMTPSender>();

            _logger.Information("Configured to send from {fromName} at {fromAddress} using {server}:{port}", _fromName, _fromAddress, _server, _port);

        }
    }
}