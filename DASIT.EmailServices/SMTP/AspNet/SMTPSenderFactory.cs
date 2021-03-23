using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using DASIT.EmailServices.AspNet;


namespace DASIT.EmailServices.SMTP.AspNet
{

    public class SMTPSenderFactory : EmailServiceFactoryAbstract, IEmailServiceFactory
    {
        private string _fromAddress { get; set; }
        private string _fromName { get; set; }
        private string _server { get; set; }
        private int _port { get; set; }



        public SMTPSenderFactory()
        {

            _fromAddress = ConfigurationManager.AppSettings["EmailFromAddress"] ?? throw new EmailSenderFactoryException("EmailFromAddress set to null");
            _fromName = ConfigurationManager.AppSettings["EmailFromName"] ?? throw new EmailSenderFactoryException("EmailFromName set to null");
            _server = ConfigurationManager.AppSettings["EmailServer"] ?? throw new EmailSenderFactoryException("EmailServer set to null");

            var tempPort = ConfigurationManager.AppSettings["EmailPort"] ?? throw new EmailSenderFactoryException("EmailPort set to null");

            _port = int.Parse(tempPort);

            _subjectPrefix = ConfigurationManager.AppSettings["EmailSubjectPrefix"] ?? "";
            _bodyPrefix = ConfigurationManager.AppSettings["EmailBodyPrefix"] ?? "";


        }


        public IEmailService GetEmailSender()
        {

            return new SMTPSender(_fromAddress, _fromName, _server, _port, _subjectPrefix, _bodyPrefix);

        }
    }
   
}