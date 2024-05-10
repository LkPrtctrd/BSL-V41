using System.Collections;
using Newtonsoft.Json;

namespace BSL.v41.Supercell.Titan.CommonUtils.HelpMap;

public class LongDictionaryKeysConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return typeof(IDictionary).IsAssignableFrom(objectType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object? existingValue,
        JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        var dictionary = (IDictionary)value!;
        writer.WriteStartObject();

        foreach (DictionaryEntry entry in dictionary)
        {
            writer.WritePropertyName(entry.Key.ToString()!);
            serializer.Serialize(writer, entry.Value);
        }

        writer.WriteEndObject();
    }
}