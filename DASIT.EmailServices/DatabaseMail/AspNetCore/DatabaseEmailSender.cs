using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Serilog;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using DASIT.EmailServices.AspNetCore;
using Newtonsoft.Json;

namespace DASIT.EmailServices.DatabaseMail.AspNetCore
{

    public class DatabaseEmailSender : DatabaseEmailSenderAbstract, IEmailService
    {

        public DatabaseEmailSender(IConfiguration configuration)
        {
           
            _profileName = configuration["EmailServices:DatabaseEmailSender:ProfileName"] ?? throw new EmailSenderException("DatabaseEmailSender ProfileName set to null");


            var tempConnectionString = configuration["EmailServices:DatabaseEmailSender:DatabaseEmailConnection"] ?? throw new EmailSenderException("DatabaseEmailSender DatabaseEmailConnection set to null");

            _databaseMailContextOptions = new DbContextOptionsBuilder<EmailContext>()
                    .UseSqlServer(tempConnectionString)
                    .Options;


            _subjectPrefix = configuration["EmailServices:SubjectPrefix"] ?? "";
            _bodyPrefix = configuration["EmailServices:BodyPrefix"] ?? "";

            // Needed to serialized of MailMessage Objects in logger

            JsonConvert.DefaultSettings = (() =>
            {
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new MailAddressConverter());
                settings.Converters.Add(new MemoryStreamConverter());
                return settings;
            });

            _logger = Log.ForContext<DatabaseEmailSender>();

            _logger.Information("Configured to use mail server profile {profileName}", _profileName);

        }

    }
}