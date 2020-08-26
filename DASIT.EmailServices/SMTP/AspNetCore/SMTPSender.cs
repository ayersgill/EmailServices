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
           
            _fromAddress = configuration["EmailServices:MailKitSender:FromAddress"] ?? throw new EmailSenderException("MailKitSender FromAddress set to null");
            _fromName = configuration["EmailServices:MailKitSender:FromName"] ?? throw new EmailSenderException("MailKitSender FromName set to null");
            _server = configuration["EmailServices:MailKitSender:Server"] ?? throw new EmailSenderException("MailKitSender Server set to null");

            var tempPort = configuration["EmailServices:MailKitSender:Port"] ?? throw new EmailSenderException("MailKitSender Port set to null");

            _port = int.Parse(tempPort);

            _subjectPrefix = configuration["EmailServices:SubjectPrefix"] ?? "";
            _bodyPrefix = configuration["EmailServices:BodyPrefix"] ?? "";

            _logger = Log.ForContext<SMTPSender>();

            _logger.Debug("Sending from {0} at {1} using {2}:{3}", _fromName, _fromAddress, _server, _port);

        }

      
    }
}