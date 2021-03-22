using System.Threading.Tasks;
using Serilog;
using System.Net.Mail;

namespace DASIT.EmailServices.Abstract
{
    public abstract class EmailSenderAbstract
    {

        protected ILogger _logger;

        protected string _subjectPrefix { get; set; }
        protected string _bodyPrefix { get; set; }

        public async Task SendHtmlEmailAsync(string recipient, string subject, string htmlMessage)
        {
            _logger.Debug("SendHtmlEmailAsync for {recipient}, Subject {subject}, HtmlMessage {htmlMessage}", recipient, subject, htmlMessage);

            MailMessage email = new MailMessage()
            {
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            email.To.Add(recipient);

            await SendEmailAsync(email);
        }

        public async Task SendHtmlEmailAsync(string[] recipients, string subject, string htmlMessage)
        {
            _logger.Debug("SendHtmlEmailAsync for {@recipient}, Subject {subject}, HtmlMessage {htmlMessage", recipients, subject, htmlMessage);

            MailMessage email = new MailMessage()
            {
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };


            foreach (string recipient in recipients)
            {
                email.To.Add(recipient);
            }

           

            await SendEmailAsync(email);
        }

        public async Task SendEmailAsync(string recipient, string subject, string textMessage)
        {
            _logger.Debug("SendEmailAsync for {recipient}, Subject {subject}, TextMessage {textMessage}", recipient, subject, textMessage);

            MailMessage email = new MailMessage()
            {
                Subject = subject,
                Body = textMessage,
                IsBodyHtml = false
            };

            email.To.Add(recipient);

            await SendEmailAsync(email);

        }

        public async Task SendEmailAsync(string[] recipients, string subject, string textMessage)
        {
            _logger.Debug("SendEmailAsync for {@recipients}, Subject {subject}, TextMessage {textMessage", recipients, subject, textMessage);

            MailMessage email = new MailMessage()
            {
                Subject = subject,
                Body = textMessage,
                IsBodyHtml = false
            };


            foreach (string recipient in recipients)
            {
                email.To.Add(recipient);
            }

            await SendEmailAsync(email);

        }

        public string[] GetArrayOfToAddresses(MailMessage mailMessage)
        {
            _logger.Debug("GetArrayOfToAddresses called with MailMessage {@mailMessage}", mailMessage);

            string[] emailAddressArray = new string[mailMessage.To.Count];

            int counter = 0;
            foreach (var recipient in mailMessage.To)
            {
                emailAddressArray[counter] = recipient.Address;
                counter++;
            }

            _logger.Debug("Returning Addresses {@emailAddressArray}", emailAddressArray);

            return emailAddressArray;
        }


        public abstract Task SendEmailAsync(MailMessage mailMessage);


    }
}