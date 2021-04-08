using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Mail;
using Newtonsoft.Json;

namespace DASIT.EmailServices
{

        public class MailAddressConverter : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var mailAddress = value as MailAddress;
                writer.WriteValue(mailAddress == null ? string.Empty : mailAddress.ToString());
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var text = reader.Value as string;
                MailAddress mailAddress;

                return IsValidMailAddress2(text, out mailAddress) ? mailAddress : null;
            }

            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(MailAddress);
            }

            private static bool IsValidMailAddress2(string text, out MailAddress value)
            {
                try
                {
                    value = new MailAddress(text);
                    return true;
                }
                catch
                {
                    value = null;
                    return false;
                }
            }
        }
    
}