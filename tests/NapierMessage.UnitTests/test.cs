using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;
using NapierMessage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace NapierMessage.UnitTests;
public class TweetProcessorTests
{

    public TweetProcessorTests()
    {
    }

    [Fact]
    public void ProcessTweet_WithTextspeak_ExpandsTextspeak()
    {
        // Arrange
        var tweet = new Tweet
        {
            
             
            
        };

        // Act
        Message.ProcessTweet(tweet);

        // Assert
        //Assert.Contains("<Later>", tweet);
    }

    [Fact]
    public void ProcessTweet_WithHashtag_AddsToHashtagList()
    {
        // Arrange
        var tweet = new Tweet
        {
            Sender = "@jane",
            Text = "Excited for #summervacation"
        };

        // Act
        processor.Process(tweet);

        // Assert
        Assert.Contains("#summervacation", processor.HashtagList);
        Assert.Equal(1, processor.HashtagList["#summervacation"]);
    }

    [Fact]
    public void ProcessTweet_WithMention_AddsToMentionList()
    {
        // Arrange
        var tweet = new Tweet
        {
            Sender = "@john",
            Text = "Hey @jane check this out"
        };

        // Act
        processor.Process(tweet);

        // Assert
        Assert.Contains("@jane", processor.MentionList);
    }
}