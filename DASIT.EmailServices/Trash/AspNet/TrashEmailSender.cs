using Serilog;
using DASIT.EmailServices.AspNet;

namespace DASIT.EmailServices.Trash.AspNet
{
    public class TrashEmailSender : TrashEmailSenderAbstract, IEmailService
    {

        public TrashEmailSender()
        {


            _logger = Log.ForContext<TrashEmailSender>();


        }

    }
}