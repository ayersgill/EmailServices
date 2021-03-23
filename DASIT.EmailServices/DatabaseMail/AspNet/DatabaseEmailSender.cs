using Serilog;
using DASIT.EmailServices.AspNet;

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

            _logger = Log.ForContext<DatabaseEmailSender>();

            _logger.Information("Configured to use mail server profile {profileName}", _profileName);

        }

    }
}