using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Serilog;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using DASIT.EmailServices.AspNetCore;

namespace DASIT.EmailServices.DatabaseMail.AspNetCore
{

    public class DatabaseEmailSender : DatabaseEmailSenderAbstract, IEmailService
    {

        public DatabaseEmailSender(IConfiguration configuration)
        {
           
            _profileName = configuration["EmailServices:DatabaseEmailSender:ProfileName"];


            _databaseMailContextOptions = new DbContextOptionsBuilder<EmailContext>()
                    .UseSqlServer(configuration["EmailServices:DatabaseEmailSender:DatabaseEmailConnection"])
                    .Options;

            _logger = Log.ForContext<DatabaseEmailSender>();

            _logger.Debug("Using mail server profile {0}", _profileName);

        }

    }
}