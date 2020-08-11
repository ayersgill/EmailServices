using System;
using System.Collections.Generic;
using System.Text;

namespace DASIT.EmailServices.Factory
{

    public abstract class EmailServiceFactory
    {
        public abstract IEmailService GetEmailSender();
    }

}