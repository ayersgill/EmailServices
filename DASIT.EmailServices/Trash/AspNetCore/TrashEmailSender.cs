using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Serilog;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using DASIT.EmailServices.AspNetCore;
using Newtonsoft.Json;

namespace DASIT.EmailServices.Trash.AspNetCore
{
    public class TrashEmailSender : TrashEmailSenderAbstract, IEmailService
    {


        public TrashEmailSender(IConfiguration configuration)
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