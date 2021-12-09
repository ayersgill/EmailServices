NOTE: View this file in a text viewer in the areas regarding using HTML tags for line breaks, 
the text version is the correct version, viewing this file in devops will try to render the tags and give you wrong info.

Webslurper Note:

Webslurper is running on wddaswebl03.ad.state.or.us on ports 25 (for smtp connections) and  8090 and 8095

http://wddaswebl03.ad.state.or.us:8090/ Is the correct url to get to the UI to view the emailService

Supported Email Senders:
Database Mail (DASIT.EmailServices.DatabaseMail.AspNetCore.DatabaseEmailSender or DASIT.EmailServices.DatabaseEmail.AspNet.DatabaseEmailSender)
SMTP (DASIT.EmailServices.SMTP.AspNetCore.SMTPSender or DASIT.EmailServices.SMTP.AspNet.SMTPSender)
Email API (DASIT.EmailServices.EmailAPI.AspNetCore.EmailAPISender or DASIT.EmailServices.EmailAPI.AspNet.EmailAPISender)
Trash (DASIT.EmailServices.Trash.AspNetCore.TrashEmailSender or DASIT.EmailServices.Trash.AspNet.TrashEmailSender)

Solution Organization
---------------------

DASIT.EmailServices:
This is the good stuff and is the contents of the produced library file.

EmailServicesContainedDemo:
Very basic .NET Core 3.1 web app for sending email by including the DASIT.EmailServices project.  Use this to test immediate changes to the EmailServices.

EmailServicesContainedNugetDemo:
Very basic .NET Core 3.1 web app for sending email by including the DASIT.EmailServices nuget from the devops artifact feed.  Use this to observe the required libraries 
and test functionality of the nuget library.

.NET Core Usage
--------------

Startup.cs

public void ConfigureServices(IServiceCollection services) {

    .....
    
    services.AddEmailServices(Configuration["EmailServices:ServiceImplementation"]);
   
    .....
}

This will use DI to make available a IEmailService object.  So, in a razor page:

private readonly IEmailService _emailService;

public IndexModel(IEmailService emailService)
{
    _emailService = emailService;
}

And then when you wish to send an email:

 try {
    await _emailService.SendEmailAsync(<Appropriate arguments, see Interface documentation>);
} catch (EmailSenderException ex) {
    Log.Error(ex, "Email Exception");
}


.NET Core Configuration
-----------------------

EmailServices must be configured correctly in the appsettings files.

There must be an EmailServices block off the root, and there must be the full Class name of the sender you
wish to use in the application.  The sender is loaded by name, so take care to make sure you ahve the 
correct name.

The SubjectPrefix and BodyPrefix entries are optional.  Line breaks in the BodyPrefix can be either \n or <br>
EmailServices will check it and convert it correctly based on the email type being sent (HTML or TEXT)

NOTE: Each sender looks for the required settings in a block of its class name.  That is why the 
configuration below, while containing configuration information for three different senders, is correct
because the EmailAPISender sender will be loaded, and it looks for its settings at EmailServices:EmailAPISender

The Profile Name for the DatabaseEmailSender must be worked out with the Database Administrator, the profile
used determines who the email appears to be from.

"EmailServices": {
    "ServiceImplementation": "DASIT.EmailServices.EmailAPI.AspNetCore.EmailAPISender",
	"SubjectPrefix": "TEST - ",
    "BodyPrefix":  "This is from test server<br><br>",
    "MailKitSender": {
      "FromAddress": "demo@oregon.gov",
      "FromName": "Demo Sender",
      "Server": "10.107.129.154",
      "Port": "2500"
    },
    "DatabaseEmailSender": {
      "DatabaseEmailConnection": "Server=wpdasclr04c.ad.state.or.us;Database=msdb;Trusted_Connection=True",
      "ProfileName": "Demo_Profile"
    },
    "EmailAPISender": {
      "FromAddress": "ACH.Coordinator@oregon.gov",
      "APISendUrl": "https://emailapi-dev.dasapp.state.or.us/send",
      "SecurityToken": "5908D47C-85D3-4024-8C2B-6EC9464398AD"
    }
  }






