using NapierMessage.SharedKernel.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NapierMessage.Core.Models;
public class Sms : Message
{
    public static JsonDocument ProcessSms(Message message)
    {
        var sms = new Sms()
        {
            Id = message.Id,
            Type = message.Type,
            Body = message.Body

        };

        sms.Body.MessageText = AbbreviationsUtils.ExpandTextspeak(sms.Body.MessageText);

        var smsJson = JsonSerializer.SerializeToDocument(sms, JsonUtils.JsonOptions);

        return smsJson;
    }
}
