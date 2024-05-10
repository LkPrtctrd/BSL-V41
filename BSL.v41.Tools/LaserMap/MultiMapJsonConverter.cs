using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BSL.v41.Tools.LaserMap;

public class MultiMapJsonConverter<TKey, TValue> : JsonConverter where TKey : notnull
{
    public override bool CanRead => false;

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(MultiMap<TKey, TValue>);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object? existingValue,
        JsonSerializer serializer)
    {
        throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        var m = (MultiMap<TKey, TValue>)value!;

        foreach (var key in m.Keys)
        foreach (var val in m[key])
        {
            writer.WritePropertyName(key.ToString()!);
            if (val != null) JToken.FromObject(val).WriteTo(writer);
        }

        writer.WriteEndObject();
    }
}