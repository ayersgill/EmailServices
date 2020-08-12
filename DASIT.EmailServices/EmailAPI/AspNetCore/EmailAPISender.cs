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

            _url = configuration["EmailServices:EmailAPISender:APISendUrl"];
            _token = configuration["EmailServices:EmailAPISender:SecurityToken"];
            _fromAddress = configuration["EmailServices:EmailAPISender:FromAddress"];

            _logger = Log.ForContext<EmailAPISender>();

            _logger.Debug("Using mail api url {0} with token {1}", _url, _token);

        }

    }
}