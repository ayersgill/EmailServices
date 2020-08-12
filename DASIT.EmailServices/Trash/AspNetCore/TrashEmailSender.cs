using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Serilog;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using DASIT.EmailServices.AspNetCore;

namespace DASIT.EmailServices.Trash.AspNetCore
{
    public class TrashEmailSender : TrashEmailSenderAbstract, IEmailService
    {


        public TrashEmailSender(IConfiguration configuration)
        {


            _logger = Log.ForContext<TrashEmailSender>();


        }

    }
}