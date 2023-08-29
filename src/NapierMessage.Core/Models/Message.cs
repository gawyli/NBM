using NapierMessage.SharedKernel.Entities;
using NapierMessage.SharedKernel.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace NapierMessage.Core.Models;

public class MessageBody
{
    public string Sender { get; set; } = null!;
    public string? Subject { get; set; }
    public string MessageText { get; set; } = string.Empty;
    

}
public class Message : BaseMessage
{
    public string Type { get; set; } = null!;
    public MessageBody Body { get; set; }

    public static JsonDocument ProcessEmail(Message email)
    {
        // Parse subject
        string subject = email.Body.Subject;

        // Check if SIR email
        if (subject.StartsWith("SIR"))
        {
            //LogSIR(email);
        }

        var emailJson = JsonSerializer.SerializeToDocument(email, JsonUtils.JsonOptions);

        return emailJson;
    }
    public static JsonDocument ProcessTweet(Message tweet)
    {
        // Parse tweet text
        string message = tweet.Body.MessageText;

        // Expand textspeak abbreviations
        message = AbbreviationsUtils.ExpandTextspeak(message);

        // Analyze hashtags
        AnalyseUtils.AnalyseHashtags(message);

        // Analyze mentions
        AnalyseUtils.AnalyseMentions(message);

        // Update Tweet object
        tweet.Body.MessageText = message;

        var tweetJson = JsonSerializer.SerializeToDocument(tweet, JsonUtils.JsonOptions);

        return tweetJson;
    }

    public static JsonDocument ProcessSms(Message sms)
    {
        // Parse message text
        string message = sms.Body.MessageText;

        // Expand textspeak abbreviations
        message = AbbreviationsUtils.ExpandTextspeak(message);

        // Update SMS object
        sms.Body.MessageText = message;

        var smsJson = JsonSerializer.SerializeToDocument(sms, JsonUtils.JsonOptions);

        return smsJson;

    }
}


