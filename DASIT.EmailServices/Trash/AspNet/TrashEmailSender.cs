using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Serilog;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using DASIT.EmailServices.AspNet;

namespace DASIT.EmailServices.Trash.AspNet
{
    public class TrashEmailSender : TrashEmailSenderAbstract, IEmailService
    {

        public TrashEmailSender()
        {
            _logger = Log.ForContext<TrashEmailSender>();

            _logger.Information("Using Trash for Email");

        }

    }
}