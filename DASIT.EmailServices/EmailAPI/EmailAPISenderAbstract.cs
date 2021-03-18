using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Serilog;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Net.Http;
using Flurl.Http;
using DASIT.EmailServices.Abstract;
using System.Net.Mail;
using System;

namespace DASIT.EmailServices.EmailAPI
{
    public abstract class EmailAPISenderAbstract : EmailSenderAbstract
    {
       
        protected string _url { get; set; }
        protected string _token { get; set; }
        protected string _fromAddress { get; set; }

       

        public override async Task SendEmailAsync(MailMessage mailMessage)
        {

            _logger.Debug("SendEmailAsync {@mailMessage}", mailMessage);

            var emailMessage = new EmailMessage
            {
                From = _fromAddress,
                To = GetArrayOfToAddresses(mailMessage),
                Subject = _subjectPrefix + mailMessage.Subject,
                Body = _bodyPrefix + mailMessage.Body

            };

            if(mailMessage.From != null)
            {
                emailMessage.From = mailMessage.From.Address;
            } else
            {
                emailMessage.From = _fromAddress;
            }

            bool result = false;

            _logger.Debug("Sending EmailMessage {@emailMessage}", emailMessage);

            try
            {

                result = await (_url)
                   .WithHeader("X-Api-Key", _token)
                   .PostJsonAsync(emailMessage)
                   .ReceiveJson<bool>();

            } catch (FlurlHttpException ex)
            {
                _logger.Error(ex, "HTTP Error");

                throw new EmailSenderException("Flurl Exception sending email.", ex);
            }

            if(!result)
            {
                _logger.Error("Error Sending Email to Email API");

                throw new EmailSenderException("Failed to send email.");
            }

        }
    }
}