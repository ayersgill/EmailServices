using Serilog;
using DASIT.EmailServices.AspNet;

namespace DASIT.EmailServices.SMTP.AspNet
{
    public class SMTPSender : SMTPSenderAbstract, IEmailService
    {


        public SMTPSender(string FromAddress, string FromName, string Server, int Port, string SubjectPrefix, string BodyPrefix)
        {

            _fromAddress = FromAddress;
            _fromName = FromName;
            _server = Server;
            _port = Port;
            _subjectPrefix = SubjectPrefix;
            _bodyPrefix = BodyPrefix;


            _logger = Log.ForContext<SMTPSender>();

            _logger.Information("Configured to send from {fromName} at {fromAddress} using {server}:{port}", _fromName, _fromAddress, _server, _port);

        }

    }
}