
using Microsoft.EntityFrameworkCore;

namespace DASIT.EmailServices.DatabaseMail
{
    public class EmailContext : DbContext
    {
       
        public EmailContext() : base() { }

        public EmailContext(DbContextOptions<EmailContext> options)
            : base(options) { }

      
    }
}
