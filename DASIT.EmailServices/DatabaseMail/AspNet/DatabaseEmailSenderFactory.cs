using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using DASIT.EmailServices.AspNet;


namespace DASIT.EmailServices.DatabaseMail.AspNet
{

    public class DatabaseEmailSenderFactory : EmailServiceFactoryAbstract, IEmailServiceFactory
    {
        private string _profileName { get; set; }
        private string _databaseConnection { get; set; }


        public DatabaseEmailSenderFactory()
        {

            _profileName = ConfigurationManager.AppSettings["EmailProfileName"] ?? throw new EmailSenderFactoryException("EmailProfileName set to null");
            _databaseConnection = ConfigurationManager.AppSettings["EmailDatabaseConnection"] ?? throw new EmailSenderFactoryException("EmailDatabaseConnection set to null");

            _subjectPrefix = ConfigurationManager.AppSettings["EmailSubjectPrefix"] ?? "";
            _bodyPrefix = ConfigurationManager.AppSettings["EmailBodyPrefix"] ?? "";

            // needed because config manager escapes \
            _bodyPrefix = _bodyPrefix.Replace("\\n", "\n");


        }


        public IEmailService GetEmailSender()
        {

            return new DatabaseEmailSender(_profileName, _databaseConnection, _subjectPrefix, _bodyPrefix);

        }
    }
   
}