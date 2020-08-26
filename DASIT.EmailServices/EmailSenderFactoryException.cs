using System;
using System.Collections.Generic;
using System.Text;

namespace DASIT.EmailServices
{
    public class EmailSenderFactoryException : Exception
    {
        public EmailSenderFactoryException()
        {
        }

        public EmailSenderFactoryException(string message)
            : base(message)
        {
        }

        public EmailSenderFactoryException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

}
