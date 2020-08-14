using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DASIT.EmailServices.AspNetCore
{
    public interface IEmailService : IEmailSender
    {

        Task SendHtmlEmailAsync(string recipient, string subject, string htmlMessage);
        Task SendHtmlEmailAsync(string[] recipients, string subject, string htmlMessage);
        Task SendEmailAsync(string[] recipients, string subject, string textMessage);
        Task SendEmailAsync(MailMessage mailMessage);

        //Inherits Task SendEmailAsync(string recipient, string subject, string textMessage) form IEmailSender


    }
}