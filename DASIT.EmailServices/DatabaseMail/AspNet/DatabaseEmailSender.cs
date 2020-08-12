using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Serilog;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using DASIT.EmailServices.AspNet;

namespace DASIT.EmailServices.DatabaseMail.AspNet
{

    public class DatabaseEmailSender : DatabaseEmailSenderAbstract, IEmailService
    {

       
        public DatabaseEmailSender(string profileName, string databaseConnection)
        {

            _profileName = profileName;


            _databaseMailContextOptions = new DbContextOptionsBuilder<EmailContext>()
                    .UseSqlServer(databaseConnection)
                    .Options;

            _logger = Log.ForContext<DatabaseEmailSender>();

            _logger.Debug("Using mail server profile {0}", _profileName);

        }

    }
}