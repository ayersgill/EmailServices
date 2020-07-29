
using Microsoft.EntityFrameworkCore;

namespace EmailServices
{
    public class EmailContext : DbContext
    {
       
        public EmailContext() : base() { }

        public EmailContext(DbContextOptions<EmailContext> options)
            : base(options) { }

      
    }
}
