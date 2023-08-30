using NapierMessage.SharedKernel.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace NapierMessage.Core.Models;
public class Tweet : Message
{
    public List<string>? Mentions { get; set; }
    public List<string>? Hashtags { get; set; }

    public static List<string>? ExtractMentions(string text)
    {
        var mentions = new List<string>();
        Regex mentionRegex = new Regex(@"@(\w+)");

        MatchCollection matches = mentionRegex.Matches(text);

        foreach (Match match in matches)
        {

            var mention = match.Value;
            mentions.Add(mention);
        }

        if (mentions.Count != 0)
        {
            return mentions;           
        }

        return null;
    }

    public static JsonDocument ProcessTweet(Message message)
    {
        var tweet = new Tweet()
        {
            Id = message.Id,
            Type = message.Type,
            Body = message.Body
        };

        tweet.Body.MessageText = AbbreviationsUtils.ExpandTextspeak(tweet.Body.MessageText);
        tweet.Mentions = ExtractMentions(tweet.Body.MessageText);

        var hashtags = Hashtag.ExtractHashtags(tweet.Body.MessageText);

        if (hashtags.TrendingList.Count != 0) 
        {
            Hashtag.WriteHashtagsCsv(hashtags);
            tweet.Hashtags = hashtags.TrendingList.Keys.ToList();
        }

        var tweetJson = JsonSerializer.SerializeToDocument(tweet, JsonUtils.JsonOptions);

        return tweetJson;
    }
}
