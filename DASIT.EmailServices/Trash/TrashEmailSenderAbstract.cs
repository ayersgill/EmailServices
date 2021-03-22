using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Serilog;
using DASIT.EmailServices.Abstract;
using System.Net.Mail;
using Newtonsoft.Json;

namespace DASIT.EmailServices.Trash
{
    public abstract class TrashEmailSenderAbstract : EmailSenderAbstract
    {

        public override async Task SendEmailAsync(MailMessage mailMessage)
        {
            _logger.Debug("SendEmailAsync {mailMessage}", JsonConvert.SerializeObject(mailMessage));

            _logger.Warning("All emails will be sent to trash.");

            await Task.CompletedTask;
        }
    }
}