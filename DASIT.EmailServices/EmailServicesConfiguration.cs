using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DASIT.EmailServices
{
    public static class EmailServicesConfiguration
    {
        public static void AddEmailServices(this IServiceCollection services, string ServiceImplementation)
        {

            var EmailImplementation = ServiceImplementation + ",DASIT.EmailServices";

            var ImplementationType = Type.GetType(EmailImplementation);

            if (ImplementationType == null)
            {
                throw new EmailSenderException(ServiceImplementation + " implementation not found.");

            }
            else
            {
                // allows for dynamic implementation of Email Service
                services.Add(new ServiceDescriptor(serviceType: typeof(IEmailService),
                                           implementationType: Type.GetType(EmailImplementation),
                                           lifetime: ServiceLifetime.Transient));
            }

        }
    }
}
