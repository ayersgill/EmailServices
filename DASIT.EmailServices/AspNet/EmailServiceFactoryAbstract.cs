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

    public abstract class EmailServiceFactoryAbstract
    {
        protected string _subjectPrefix { get; set; }
        protected string _bodyPrefix { get; set; }
    }

}