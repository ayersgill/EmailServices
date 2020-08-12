using System;
using System.Collections.Generic;
using System.Text;

namespace DASIT.EmailServices.AspNet
{

    public interface IEmailServiceFactory
    {
        IEmailService GetEmailSender();
    }

}