﻿using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace DASIT.EmailServices.AspNet
{
    public interface IEmailService 
    {

        Task SendHtmlEmailAsync(string recipient, string subject, string htmlMessage);
        Task SendHtmlEmailAsync(string[] recipients, string subject, string htmlMessage);
        Task SendEmailAsync(string recipient, string subject, string textMessage);
        Task SendEmailAsync(string[] recipients, string subject, string textMessage);


    }
}