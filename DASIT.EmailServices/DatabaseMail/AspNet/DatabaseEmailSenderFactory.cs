using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using DASIT.EmailServices.AspNet;


namespace DASIT.EmailServices.DatabaseMail.AspNet
{

    public class DatabaseEmailSenderFactory : IEmailServiceFactory
    {
        private string _profileName { get; set; }
        private string _databaseConnection { get; set; }


        public DatabaseEmailSenderFactory()
        {

            _profileName = ConfigurationManager.AppSettings["EmailProfileName"];
            _databaseConnection = ConfigurationManager.AppSettings["EmailDatabaseConnection"];

        }


        public IEmailService GetEmailSender()
        {

            return new DatabaseEmailSender(_profileName, _databaseConnection);

        }
    }
   
}