using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using DASIT.EmailServices.AspNet;


namespace DASIT.EmailServices.EmailAPI.AspNet
{

    public class EmailAPISenderFactory :  EmailServiceFactoryAbstract, IEmailServiceFactory
    {
        private string _url { get; set; }
        private string _token { get; set; }
        private string _fromAddress { get; set; }


        public EmailAPISenderFactory()
        {

            _url = ConfigurationManager.AppSettings["EmailAPIUrl"] ?? throw new EmailSenderFactoryException("EmailAPIUrl set to null");
            _token = ConfigurationManager.AppSettings["EmailAPIToken"] ?? throw new EmailSenderFactoryException("EmailAPIToken set to null");
            _fromAddress = ConfigurationManager.AppSettings["EmailAPIFrom"] ?? throw new EmailSenderFactoryException("EmailAPIFrom set to null");
            _subjectPrefix = ConfigurationManager.AppSettings["EmailSubjectPrefix"] ?? "";
            _bodyPrefix = ConfigurationManager.AppSettings["EmailBodyPrefix"] ?? "";

            // needed because config manager escapes \
            _bodyPrefix = _bodyPrefix.Replace("\\n", "\n");



        }


        public IEmailService GetEmailSender()
        {

            return new EmailAPISender(_url, _token, _fromAddress, _subjectPrefix, _bodyPrefix);

        }
    }
   
}