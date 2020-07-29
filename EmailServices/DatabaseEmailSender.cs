using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Serilog;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace EmailServices
{
    public class DatabaseEmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        private string _profileName { get; set; }

        private DbContextOptions<EmailContext> _databaseMailContextOptions { get; set; }

        private readonly ILogger _logger;

        public DatabaseEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
            _profileName = _configuration["EmailServices:ProfileName"];


            _databaseMailContextOptions = new DbContextOptionsBuilder<EmailContext>()
                    .UseSqlServer(configuration["EmailServices:DatabaseEmailConnection"])
                    .Options;

            _logger = Log.ForContext<DatabaseEmailSender>();

            _logger.Debug("Using mail server profile {0}", _profileName);

        }


        public async Task SendHtmlEmailAsync(string recipient, string subject, string htmlMessage)
        {
            _logger.Information("SendHtmlEmailAsync Called");
            _logger.Debug("Email {0}, Subject {1}, HtmlMessage {2}", recipient, subject, htmlMessage);

            await SendEmailAsync(new string[] { recipient }, subject, "HTML", htmlMessage);
        }

        public async Task SendHtmlEmailAsync(string[] recipients, string subject, string htmlMessage)
        {
            _logger.Information("SendHtmlEmailAsync Called");
            _logger.Debug("Recipients {@0}, Subject {1}, HtmlMessage {2}", recipients, subject, htmlMessage);

            await SendEmailAsync(recipients, subject, "HTML", htmlMessage);
        }

        public async Task SendTextEmailAsync(string recipient, string subject, string textMessage)
        {
            _logger.Information("SendTextEmailAsync Called");
            _logger.Debug("Email {0}, Subject {1}, TextMessage {2}", recipient, subject, textMessage);

            await SendEmailAsync(new string[] { recipient }, subject, "TEXT", textMessage);
        }

        public async Task SendTextEmailAsync(string[] recipients, string subject, string textMessage)
        {
            _logger.Information("SendTextEmailAsync Called");
            _logger.Debug("Recipients {@0}, Subject {1}, TextMessage {2}", recipients, subject, textMessage);

            await SendEmailAsync(recipients, subject, "TEXT", textMessage);
        }

        private async Task SendEmailAsync(string[] recipients, string subject, string formatType, string message)
        {

            _logger.Information("SendEmailAsync Called");
            _logger.Debug("Recipients {@0}, Subject {1}, Format Type {2}. Message {2}", recipients, subject, formatType, message);


            // recipients


            using (var db = new EmailContext(_databaseMailContextOptions))
            {
                string recipientsString = "";

                foreach(string recipient in recipients)
                {
                    recipientsString += recipient + ";";
                }

                db.Database.ExecuteSqlCommand("ESEC sp_send_dbmail @profile_name, @recipients, @subject, @body_format, @body",  
                    new SqlParameter("profile_name", _profileName),
                    new SqlParameter("recipients", recipientsString),
                    new SqlParameter("subject", subject),
                    new SqlParameter("body_format", formatType),
                    new SqlParameter("body", message)
                );

                /*

                // Create a SQL command to execute the sproc
                var cmd = db.Database.Connection.CreateCommand();
                cmd.CommandText = "[dbo].[GetAllBlogsAndPosts]";

                try
                {

                    db.Database.Connection.Open();
                    // Run the sproc
                    var reader = cmd.ExecuteReader();

                    // Read Blogs from the first result set
                    var blogs = ((IObjectContextAdapter)db)
                        .ObjectContext
                        .Translate<Blog>(reader, "Blogs", MergeOption.AppendOnly);


                    foreach (var item in blogs)
                    {
                        Console.WriteLine(item.Name);
                    }

                    // Move to second result set and read Posts
                    reader.NextResult();
                    var posts = ((IObjectContextAdapter)db)
                        .ObjectContext
                        .Translate<Post>(reader, "Posts", MergeOption.AppendOnly);


                    foreach (var item in posts)
                    {
                        Console.WriteLine(item.Title);
                    }
                }
                finally
                {
                    db.Database.Connection.Close();
                }*/
            }





            /*


            SqlConnection connLMSSQL01 = new SqlConnection("Data Source=AAA;Initial Catalog=BBB;Persist Security Info=True;User ID=CCC;Password=***");


            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress(from));

            msg.Subject = subject;

            foreach (var email in recipients)
            {
                msg.To.Add(new MailboxAddress(email));
            }

            _logger.Debug("Sending email with subject {0} to {1}", subject, recipients.Join("; "));

            msg.Body = new TextPart("html") { Text = htmlMessage };

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(server, port, false);
                    await client.SendAsync(msg);
                    await client.DisconnectAsync(true);
                }
                catch (SocketException ex)
                {
                    _logger.Error(ex, "Error connection to SMTP Servwer");

                }
            }*/
        }
    }
}