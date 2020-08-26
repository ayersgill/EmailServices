using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Serilog;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Net.Http;
using Flurl.Http;
using DASIT.EmailServices.AspNetCore;

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

            _logger = Log.ForContext<EmailAPISender>();

            _logger.Debug("Using mail api url {0} with token {1}", _url, _token);

        }

    }
}