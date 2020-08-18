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


.NET Core Configuration
-----------------------

EmailServices must be configured correctly in the appsettings files.

There must be an EmailServices block off the root, and there must be the full Class name of the sender you
wish to use in the application.  The sender is loaded by name, so take care to make sure you ahve the 
correct name.

NOTE: Each sender looks for the required settings in a block of its class name.  That is why the 
configuration below, while containing configuration information for three different senders, is correct
because the EmailAPISender sender will be loaded, and it looks for its settings at EmailServices:EmailAPISender

The Profile Name for the DatabaseEmailSender must be worked out with the Database Administrator, the profile
used determines who the email appears to be from.

"EmailServices": {
    "ServiceImplementation": "DASIT.EmailServices.EmailAPI.AspNetCore.EmailAPISender",
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



.NET Framework Usage
-----------------------

var send = EmailServiceFactory.GetEmailSender();

await send.SendEmailAsync(<Appropriate arguments, see Interface documentation>);

Note: It is not necessary to store send object locally.  The static EmailServiceFactory will hold it after the first creation and continue to provide it.

.NET Framework Configuration
------------------------

Configuration for EmailServices must be set in the appsettings section of the web.configuration.

Here you just need to specify the classname of the senderfactory you want to use (DO NOT use the full name, just the classname)

Similar to the Net Core configuration, each sender will look for its own needed configuration values, so the ones not needed will be ignored
and not cause problems (Although not recommended for clarity).

The Profile Name for the DatabaseEmailSender must be worked out with the Database Administrator, the profile
used determines who the email appears to be from.

<appSettings>

	<add key="EmailSenderFactory" value="EmailAPISenderFactory"/> // or SMTPSenderFactory or DatabaseEmailSenderFactory
    <add key="EmailAPIUrl" value="https://emailapi-dev.dasapp.state.or.us/send" />
    <add key="EmailAPIToken" value="5908D47C-85D3-4024-8C2B-6EC9464398AD" />
    <add key="EmailAPIFrom" value="ACH.Coordinator@oregon.gov" />

	// SMTP Sender settings
	<add key="EmailFromAddress" value="demo@oregon.gov" />
	<add key="EmailFromName" value="Demo Sender" />
	<add key="EmailServer" value="10.107.129.154" />
	<add key="EmailPort" value="2500" />

	// Database Mail Settings
	<add key="EmailProfileName" value="Demo_Profile" />
	<add key="EmailDatabaseConnection" value="Server=wpdasclr04c.ad.state.or.us;Database=msdb;Trusted_Connection=True" />

	
	
  </appSettings>
  
  





