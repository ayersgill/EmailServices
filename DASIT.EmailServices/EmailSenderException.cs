using System;
using System.Collections.Generic;
using System.Text;

namespace DASIT.EmailServices
{
    public class EmailSenderException : Exception
    {
        public EmailSenderException()
        {
        }

        public EmailSenderException(string message)
            : base(message)
        {
        }

        public EmailSenderException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

}
