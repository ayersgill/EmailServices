using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Serilog;
using DASIT.EmailServices.Abstract;

namespace DASIT.EmailServices.Trash
{
    public abstract class TrashEmailSenderAbstract : EmailSenderAbstract
    {



        public override async Task SendEmailAsync(string[] recipients, string subject, string formatType, string message)
        {

            _logger.Information("SendEmailAsync Called");
            _logger.Debug("Recipients {@0}, Subject {1}, Format Type {2}. Message {2}", recipients, subject, formatType, message);


            _logger.Warning("All emails will be sent to trash.");

            await Task.CompletedTask;
        }
    }
}