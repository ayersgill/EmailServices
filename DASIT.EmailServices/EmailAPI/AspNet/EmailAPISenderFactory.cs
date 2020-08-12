using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using DASIT.EmailServices.AspNet;


namespace DASIT.EmailServices.EmailAPI.AspNet
{

    public class EmailAPISenderFactory : IEmailServiceFactory
    {
        private string _url { get; set; }
        private string _token { get; set; }
        private string _fromAddress { get; set; }

        public EmailAPISenderFactory()
        {

            _url = ConfigurationManager.AppSettings["EmailAPIUrl"];
            _token = ConfigurationManager.AppSettings["EmailAPIToken"];
            _fromAddress = ConfigurationManager.AppSettings["EmailAPIFrom"];


        }


        public IEmailService GetEmailSender()
        {

            return new EmailAPISender(_url, _token, _fromAddress);

        }
    }
   
}