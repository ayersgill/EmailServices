using Serilog;
using DASIT.EmailServices.AspNet;
using Newtonsoft.Json;

namespace DASIT.EmailServices.DatabaseMail.AspNet
{

    public class DatabaseEmailSender : DatabaseEmailSenderAbstract, IEmailService
    {

       
        public DatabaseEmailSender(string profileName, string databaseConnection, string SubjectPrefix, string BodyPrefix)
        {

            _profileName = profileName;

            _databaseConnectionString = databaseConnection;

            _subjectPrefix = SubjectPrefix;
            _bodyPrefix = BodyPrefix;

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