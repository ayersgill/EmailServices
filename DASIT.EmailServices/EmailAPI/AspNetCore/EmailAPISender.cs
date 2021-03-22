using Microsoft.Extensions.Configuration;
using Serilog;
using DASIT.EmailServices.AspNetCore;
using Newtonsoft.Json;

namespace DASIT.EmailServices.EmailAPI.AspNetCore
{
    public class EmailAPISender : EmailAPISenderAbstract, IEmailService
    {
      
        public EmailAPISender(IConfiguration configuration)
        {

            _url = configuration["EmailServices:EmailAPISender:APISendUrl"] ?? throw new EmailSenderException("EmailAPI APISendUrl set to null");
            _token = configuration["EmailServices:EmailAPISender:SecurityToken"] ?? throw new EmailSenderException("EmailAPI SecurityToken set to null");
            _fromAddress = configuration["EmailServices:EmailAPISender:FromAddress"] ?? throw new EmailSenderException("EmailAPI FromAddress set to null");
            _subjectPrefix = configuration["EmailServices:SubjectPrefix"] ?? "";
            _bodyPrefix = configuration["EmailServices:BodyPrefix"] ?? "";

            JsonConvert.DefaultSettings = (() =>
            {
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new MailAddressConverter());
                settings.Converters.Add(new MemoryStreamConverter());
                return settings;
            });

            _logger = Log.ForContext<EmailAPISender>();

            _logger.Information("Configured to use {api_url} with token {token}", _url, _token);

        }

    }
}