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
            return null;
        }

        public override void Write(
            Utf8JsonWriter writer,
            MailMessage message,
            JsonSerializerOptions options)
        {
            writer.WriteStringValue("foo");
        }


    }
}
