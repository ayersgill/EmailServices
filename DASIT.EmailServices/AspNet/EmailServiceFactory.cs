using DASIT.EmailServices.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DASIT.EmailServices.AspNet
{

    public abstract class EmailServiceFactory
    {
        public abstract IEmailService GetEmailSender();
    }

}