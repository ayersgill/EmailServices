using Serilog;
using DASIT.EmailServices.AspNet;
using Newtonsoft.Json;

namespace DASIT.EmailServices.Trash.AspNet
{
    public class TrashEmailSender : TrashEmailSenderAbstract, IEmailService
    {

        public TrashEmailSender()
        {

            JsonConvert.DefaultSettings = (() =>
            {
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new MailAddressConverter());
                settings.Converters.Add(new MemoryStreamConverter());
                return settings;
            });

            _logger = Log.ForContext<TrashEmailSender>();

            _logger.Information("Using Trash for Email");


        }

    }
}