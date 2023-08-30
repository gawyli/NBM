using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NapierMessage.SharedKernel.Utilities;
public static class AbbreviationsUtils
{
    public class AbbreviationRecords
    {
        public string Abbreviation { get; set; } = null!;
        public string Expansion { get; set; } = null!;
    }

    public static string ExpandTextspeak(string text)
    {
        var path = @"..\NapierMessage.Channel.Web\AbbreviationRecords.csv";
        Dictionary<string, string> abbreviations = LoadAbbreviationMap(path);

        foreach (var abbreviation in abbreviations)
        {
            string pattern = $@"(?<=^|\s|[^\w]){abbreviation.Key}(?=\s|[^\w]|$)";
            string replacement = $"<{abbreviation.Value}>";

            text = Regex.Replace(text, pattern, replacement);
        }

        return text;
    }

    private static Dictionary<string, string> LoadAbbreviationMap(string csvFilePath)
    {
        Dictionary<string, string> map = new Dictionary<string, string>();

        try
        {
            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var records = csv.GetRecords<AbbreviationRecords>();
                foreach (var record in records)
                {
                    // Add the abbreviation and full form to the dictionary
                    map[record.Abbreviation] = record.Expansion;
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions that may occur during file reading or CSV parsing.
            Console.WriteLine("An error occurred: " + ex.Message);
        }

        return map;
    }
}

