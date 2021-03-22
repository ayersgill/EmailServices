using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Mail;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DASIT.EmailServices.AspNetCore
{


    public class MailAddressJsonConverter : JsonConverter<MailAddress>
    {
        public override void Write(Utf8JsonWriter writer,
          MailAddress mailAddress,
          JsonSerializerOptions options)
        {
            writer.WriteStringValue(mailAddress == null ? string.Empty : mailAddress.ToString());
        }


        public override MailAddress Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions options)
        {
            var text = reader.GetString();
            MailAddress mailAddress;

            return IsValidMailAddress(text, out mailAddress) ? mailAddress : null;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(MailAddress);
        }

        private static bool IsValidMailAddress(string text, out MailAddress value)
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