using DASIT.EmailServices.DatabaseMail.AspNet;
using DASIT.EmailServices.EmailAPI.AspNet;
using DASIT.EmailServices.SMTP.AspNet;
using DASIT.EmailServices.Trash.AspNet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace DASIT.EmailServices.AspNet
{

    public static class EmailServiceFactory
    {
        private static IEmailService _emailSender;

        private static IEmailServiceFactory _emailSenderFactory;

        static EmailServiceFactory()
        {

            var senderFactoryName = ConfigurationManager.AppSettings["EmailSenderFactory"];

            switch (senderFactoryName)
            {
                case "SMTPSenderFactory":
                    _emailSenderFactory = (IEmailServiceFactory) new SMTPSenderFactory();
                    break;
                case "EmailAPISenderFactory":
                    _emailSenderFactory = (IEmailServiceFactory) new EmailAPISenderFactory();
                    break;
                case "DatabaseEmailSenderFactory":
                    _emailSenderFactory = (IEmailServiceFactory) new DatabaseEmailSenderFactory();
                    break;
                default:
                    _emailSenderFactory = (IEmailServiceFactory) new TrashSenderFactory();
                    break;

            }

            _emailSender = _emailSenderFactory.GetEmailSender();
        }

        public static IEmailService GetEmailSender()
        {
            return _emailSender;
        }
    }

}