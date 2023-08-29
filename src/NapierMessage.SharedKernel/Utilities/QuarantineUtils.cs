using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NapierMessage.SharedKernel.Utilities;
public static class QuarantineUtils
{
    public static string QuarantineURLs(string text)
    {

        Regex urlRegex = new Regex("http[s]?://(?:[a-zA-Z]|[0-9]|[$-_@.&+]|[!*(),]|(?:%[0-9a-fA-F][0-9a-fA-F]))+");

        return urlRegex.Replace(text, "<URL Quarantined>");

    }
}
