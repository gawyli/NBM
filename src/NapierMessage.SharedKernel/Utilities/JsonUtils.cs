using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NapierMessage.SharedKernel.Utilities;
public static class JsonUtils
{
    public static JsonSerializerOptions JsonOptions => new JsonSerializerOptions
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public static void WriteJsonToFile(this JsonDocument jsonObj, string folderPath)
    {
        using var fileStream = File.Create(folderPath);
        using var utf8JsonWriter = new Utf8JsonWriter(fileStream);
        jsonObj.WriteTo(utf8JsonWriter);
    }
}
