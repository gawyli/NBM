using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;
using NapierMessage.Core.Models;
using NapierMessage.SharedKernel.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace NapierMessage.UnitTests;
public class TweetTests
{
    [Fact]
    public void TestProcessTweet()
    {
        var message = new Message()
        {
            Id = "1",
            Type = "Tweet",
            Body = new MessageBody()
            {
                MessageText = "Testing @mention and #hashtag"
            }
        };

        var expectedJson = JsonSerializer.Serialize(message, JsonUtils.JsonOptions);

        var actualJson = Tweet.ProcessTweet(message);
        var actualJsonString = JsonSerializer.Serialize(actualJson);

        Assert.Equivalent(expectedJson, actualJsonString);
    }
}