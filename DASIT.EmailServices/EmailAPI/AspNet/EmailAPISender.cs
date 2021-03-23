using Serilog;
using DASIT.EmailServices.AspNet;

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

            _logger = Log.ForContext<EmailAPISender>();

            _logger.Information("Configured to use mail api url {api_url} with token {token}", _url, _token);

        }

    }
}