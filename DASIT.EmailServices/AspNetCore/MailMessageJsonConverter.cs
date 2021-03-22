using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Mail;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DASIT.EmailServices.AspNetCore
{

    public class MailMessageJsonConverter : JsonConverter<MailMessage> {

        public override MailMessage Read(
           ref Utf8JsonReader reader,
           Type typeToConvert,
           JsonSerializerOptions options)
        {
            //No need for this library to ever read in a json string and populate MailMessage
            return null;
        }

        public override void Write(
            Utf8JsonWriter writer,
            MailMessage message,
            JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            if(message.From != null)
            {
                writer.WriteStartObject("From");
                writer.WriteString(JsonEncodedText.Encode("DisplayName"), message.From.DisplayName);

            }
        }


    }
}
