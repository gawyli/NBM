using NapierMessage.Core.Models;
using NapierMessage.SharedKernel.Entities;
using NapierMessage.SharedKernel.Interfaces;
using NapierMessage.SharedKernel.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NapierMessage.Infrastructure.Repositories;
public class MessageRepository : IMessageRepository
{
    public async Task WriteToJsonFile(JsonDocument messageJson, CancellationToken cancellationToken)
    {
        Message message = JsonSerializer.Deserialize<Message>(messageJson);

        string messageId = message.Id;
        string messageType = message.Type;

        // Get the base directory of the application
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        // Combine it with the relative path to the "Messages" folder
        string folderPath = Path.Combine(baseDirectory, "Messages");

        try
        {
            string catalogPath = CreateCatalog(folderPath, messageType);
            string filePath = Path.Combine(catalogPath, $"{messageId}.json");

            messageJson.WriteJsonToFile(filePath);

            //string message2 = JsonSerializer.Serialize(messageJson);
            //await File.WriteAllTextAsync(filePath, message2, cancellationToken);

            

            Console.WriteLine($"JSON data saved to {filePath}");
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to save Json: " + ex.Message);
        }
    }

    private string CreateCatalog(string folderPath, string messageType)
    {
        if (messageType == "Email")
        {
            string emailFolder = Path.Combine(folderPath, "emails");
            CreateDirectory(emailFolder);
            return emailFolder;
        }
        else if (messageType == "Sms")
        {
            string smsFolder = Path.Combine(folderPath, "sms");
            CreateDirectory(smsFolder);
            return smsFolder;
        }
        else
        {
            string tweetFolder = Path.Combine(folderPath, "tweets");
            CreateDirectory(tweetFolder);
            return tweetFolder;
        }
    }

    private void CreateDirectory(string folder)
    {
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
            Console.WriteLine($"Folder '{folder}' created.");
        }
        else
        {
            Console.WriteLine($"Folder '{folder}' already exists.");
        }
    }
}
