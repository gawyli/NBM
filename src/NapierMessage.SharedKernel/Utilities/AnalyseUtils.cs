using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NapierMessage.SharedKernel.Utilities;
public static class AnalyseUtils
{
    // Analyse hashtags

    public static void AnalyseHashtags(string text)
    {

        Regex hashtagRegex = new Regex(@"#(\w+)");

        MatchCollection matches = hashtagRegex.Matches(text);

        foreach (Match match in matches)
        {
        
            string hashtag = match.Groups[1].Value;

            // Increment count for this hashtag
        }

    }

    // Analyse mentions

    public static void AnalyseMentions(string text)
    {

        Regex mentionRegex = new Regex(@"@(\w+)");

        MatchCollection matches = mentionRegex.Matches(text);

        foreach (Match match in matches)
        {
        
            string mention = match.Groups[1].Value;

            // Add mention to list
        }

    }
}
