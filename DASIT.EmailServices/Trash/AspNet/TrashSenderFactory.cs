using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using DASIT.EmailServices.AspNet;


namespace DASIT.EmailServices.Trash.AspNet
{

    public class TrashSenderFactory : IEmailServiceFactory
    {
       


        public TrashSenderFactory()
        {

           

        }


        public IEmailService GetEmailSender()
        {

            return new TrashEmailSender();

        }
    }
   
}