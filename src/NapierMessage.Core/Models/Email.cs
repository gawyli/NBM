using NapierMessage.SharedKernel.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NapierMessage.Core.Models;
public class Email : Message
{

    public static JsonDocument ProcessEmail(Message message)
    {
        // Parse subject
        string subject = message.Body.Subject;

        // Check if SIR email
        if (subject.StartsWith("SIR"))
        {
            //LogSIR(email);
        }

        var emailJson = JsonSerializer.SerializeToDocument(message, JsonUtils.JsonOptions);

        return emailJson;
    }
}
