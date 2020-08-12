﻿using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Serilog;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Net.Http;
using Flurl.Http;
using DASIT.EmailServices.AspNet;

namespace DASIT.EmailServices.EmailAPI.AspNet
{
    public class EmailAPISender : EmailAPISenderAbstract, IEmailService
    {
      
       

        public EmailAPISender(string APIUrl, string SecurityToken, string FromAddress)
        {


            _url = APIUrl;
            _token = SecurityToken;
            _fromAddress = FromAddress;

            _logger = Log.ForContext<EmailAPISender>();

            _logger.Debug("Using mail api url {0} with token {1}", _url, _token);

        }

    }
}