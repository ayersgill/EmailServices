using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using DASIT.EmailServices.AspNet;


namespace DASIT.EmailServices.SMTP.AspNet
{

    public class SMTPSenderFactory : IEmailServiceFactory
    {
        private string _fromAddress { get; set; }
        private string _fromName { get; set; }
        private string _server { get; set; }
        private int _port { get; set; }



        public SMTPSenderFactory()
        {

            _fromAddress = ConfigurationManager.AppSettings["EmailFromAddress"];
            _fromName = ConfigurationManager.AppSettings["EmailFromName"];
            _server = ConfigurationManager.AppSettings["EmailServer"];
            _port = int.Parse(ConfigurationManager.AppSettings["EmailPort"]);


        }


        public IEmailService GetEmailSender()
        {

            return new SMTPSender(_fromAddress, _fromName, _server, _port);

        }
    }
   
}