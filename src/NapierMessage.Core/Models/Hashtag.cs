using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NapierMessage.Core.Models;
public class HashtagMap : ClassMap<Hashtag>
{
    public HashtagMap()
    {
        Map(m => m.Text).Name("HashtagText"); // Map the 'Text' property to the 'HashtagText' column
        Map(m => m.Count).Name("Count");       // Map the 'Count' property to the 'Count' column
    }
}

public class Hashtag
{
    public string? Text { get; set; }
    public int Count { get; set; }

    public Dictionary<string, int> TrendingList { get; set; } = new();

    public static Hashtag ExtractHashtags(string text)
    {
        var hashtags = new Hashtag();
        string pattern = @"#\w+";

        MatchCollection matches = Regex.Matches(text, pattern);

        foreach (Match match in matches)
        {
            string hashtag = match.Value;

            if (hashtags.TrendingList.ContainsKey(hashtag))
            {
                hashtags.TrendingList[hashtag]++;
            }
            else
            {
                hashtags.TrendingList.Add(hashtag, 1);
            }
        }

        return hashtags;
    }

    public static void WriteHashtagsCsv(Hashtag hashtags)
    {
        // Define the path to the CSV file
        string filePath = "hashtags.csv";

        // Read the existing data from the CSV file
        var existingHashtags = new List<Hashtag>();

        if (File.Exists(filePath))
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                // Map the CSV columns to the 'Hashtag' class properties
                csv.Context.RegisterClassMap<HashtagMap>();

                existingHashtags = csv.GetRecords<Hashtag>().ToList();
            }
        }

        // Update the existing hashtags with new counts or add new hashtags
        foreach (var hashtag in hashtags.TrendingList.Keys)
        {
            var existingHashtag = existingHashtags.Find(h => h.Text == hashtag);
            if (existingHashtag != null)
            {
                // Update the count of the existing hashtag
                existingHashtag.Count += hashtags.TrendingList[hashtag];
            }
            else
            {
                // Add the new hashtag
                var newHashtag = new Hashtag { Text = hashtag, Count = hashtags.TrendingList[hashtag] };
                existingHashtags.Add(newHashtag);
            }
        }

        // Write the updated data back to the CSV file
        using (var writer = new StreamWriter(filePath))
        using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            // Map the 'Hashtag' class properties to the CSV columns
            csv.Context.RegisterClassMap<HashtagMap>();

            csv.WriteRecords(existingHashtags);
        }

        Console.WriteLine($"Hashtags written or updated in {filePath}");
    }

}
