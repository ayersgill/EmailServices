using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace EmailServices
{
    public interface IEmailService : IEmailSender
    {

        Task SendHtmlEmailAsync(string recipient, string subject, string htmlMessage);
        Task SendHtmlEmailAsync(string[] recipients, string subject, string htmlMessage);

        Task SendEmailAsync(string[] recipients, string subject, string textMessage);

       

    }
}