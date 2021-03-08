﻿using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Serilog;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using MimeKit;
using MimeKit.Text;
using System.Net.Mail;
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

            _logger.Debug("Configured to send from {0} at {1} using {2}:{3}", _fromName, _fromAddress, _server, _port);

        }

    }
}