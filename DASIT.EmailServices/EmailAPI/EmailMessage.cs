using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DASIT.EmailServices.EmailAPI
{

        public class EmailMessage
        {
            public string From { get; set; }
            public string[] To { get; set; } = null;
            public string[] UniqueIds { get; set; } = null;
            public string Subject { get; set; }
            public string Body { get; set; }
            public string[] Cc { get; set; } = null;
            public string[] Bcc { get; set; } = null;
            public int? Agency = null;

            public EmailMessage() { }
            public EmailMessage(string from, ICollection<string> to,
                string subject, string body, ICollection<string> cc = null,
                    ICollection<string> bcc = null)
            {
                From = from;
                To = CleanAddresses(to);
                Subject = subject;
                Body = body;
                Cc = CleanAddresses(cc);
                Bcc = CleanAddresses(bcc);
            }

            public EmailMessage(string from, ICollection<string> uniqueIds,
                 string subject, string body, int? agency = null)
            {
                From = from;
                UniqueIds = uniqueIds.ToArray();
                Subject = subject;
                Body = body;
                Agency = agency;
            }

            public string[] CleanAddresses(ICollection<string> addresses = null)
            {
                if (addresses == null)
                {
                    addresses = new string[0];
                }
                string[] result = new string[addresses.Count];
                result = addresses.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                return result;
            }
        }
    }


