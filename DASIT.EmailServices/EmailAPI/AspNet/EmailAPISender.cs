using Serilog;
using DASIT.EmailServices.AspNet;
using Newtonsoft.Json;

namespace DASIT.EmailServices.EmailAPI.AspNet
{
    public class EmailAPISender : EmailAPISenderAbstract, IEmailService
    {
      
       

        public EmailAPISender(string APIUrl, string SecurityToken, string FromAddress, string SubjectPrefix, string BodyPrefix)
        {


            _url = APIUrl;
            _token = SecurityToken;
            _fromAddress = FromAddress;
            _subjectPrefix = SubjectPrefix;
            _bodyPrefix = BodyPrefix;

            JsonConvert.DefaultSettings = (() =>
            {
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new MailAddressConverter());
                settings.Converters.Add(new MemoryStreamConverter());
                return settings;
            });

            _logger = Log.ForContext<EmailAPISender>();

            _logger.Information("Configured to use mail api url {api_url} with token {token}", _url, _token);

        }

    }
}