using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Serilog;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using MimeKit;
using MimeKit.Text;
using System.Net.Mail;
using DASIT.EmailServices.AspNetCore;

namespace DASIT.EmailServices.SMTP.AspNetCore
{
    public class SMTPSender : SMTPSenderAbstract, IEmailService
    {
       

        public SMTPSender(IConfiguration configuration)
        {
           
            _fromAddress = configuration["EmailServices:MailKitSender:FromAddress"];
            _fromName = configuration["EmailServices:MailKitSender:FromName"];
            _server = configuration["EmailServices:MailKitSender:Server"];
            _port = int.Parse(configuration["EmailServices:MailKitSender:Port"]);


            _logger = Log.ForContext<SMTPSender>();

            _logger.Debug("Sending from {0} at {1} using {2}:{3}", _fromName, _fromAddress, _server, _port);

        }

      
    }
}