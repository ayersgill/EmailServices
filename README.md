Supported Email Senders:
Database Mail
SMTP
Email API

Solution Organization
---------------------

DASIT.EmailServices:
This is the good stuff and is the contents of the produced library file.

EmailFactoryDemo:
Very basic .NET Framework 4.7.2 web app for sending email by including the DASIT.EmailServices project.  Use this to test immediate changes to the EmailServices.  

EmailFactoryNugetDemo:
Very basic .NET Framework 4.7.2 web app for sending email by including the DASIT.EmailServices nuget from the devops artifact feed.  Use this to observe the required libraries 
and test functionality of the nuget library.

EmailServicesDemo:
Very basic .NET Core 2.1 web app for sending email by including the DASIT.EmailServices project.  Use this to test immediate changes to the EmailServices.

EmailServicesNugetDemo:
Very basic .NET Core 2.1 web app for sending email by including the DASIT.EmailServices nuget from the devops artifact feed.  Use this to observe the required libraries 
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


.NET Framework Usage
-----------------------

var send = EmailServiceFactory.GetEmailSender();

await send.SendEmailAsync(<Appropriate arguments, see Interface documentation>);

Note: It is not necessary to store send object locally.  The static EmailServiceFactory will hold it after the first creation and continue to provide it.





