using System.Threading.Tasks;

namespace EmailServices
{
    public interface IEmailSender
    {

        Task SendHtmlEmailAsync(string recipient, string subject, string htmlMessage);
        Task SendHtmlEmailAsync(string[] recipients, string subject, string htmlMessage);

        Task SendTextEmailAsync(string recipient, string subject, string textMessage);
        Task SendTextEmailAsync(string[] recipients, string subject, string textMessage);

       

    }
}