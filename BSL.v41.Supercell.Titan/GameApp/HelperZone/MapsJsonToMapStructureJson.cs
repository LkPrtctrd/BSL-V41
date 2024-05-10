using Newtonsoft.Json;

namespace BSL.v41.Supercell.Titan.GameApp.HelperZone;

public class MapsJsonToMapStructureJson
{
    public static void ConvertAndWrite(string path)
    {
        var json = File.ReadAllText(path);
        var jsonDataList = JsonConvert.DeserializeObject<List<JsonData>>(json);

        string previousMapValue = null!;
        {
            for (var i = 0; i < jsonDataList!.Count; i++)
                if (string.IsNullOrEmpty(jsonDataList[i].Map))
                    jsonDataList[i].Map = previousMapValue;
                else
                    previousMapValue = jsonDataList[i].Map!;
        }

        dynamic jsonArray =
            JsonConvert.DeserializeObject(JsonConvert.SerializeObject(jsonDataList, Formatting.Indented))!;
        var data = new List<(string Map, string Data, string MetaData)>();

        foreach (var item in jsonArray)
        {
            string map = item.Map;
            string mapData = item.Data;
            string metaData = item.MetaData;
            data.Add((map, mapData, metaData));
        }

        var groupedData = data.GroupBy(d => d.Map)
            .Select(g => new
            {
                Map = g.Key,
                Data = string.Join("", g.Select(d => d.Data)),
                MetaData = string.Join("", g.Select(d => d.MetaData))
            });

        File.WriteAllText(path,
            JsonConvert.SerializeObject(groupedData.ToArray()));
    }
}

public sealed class JsonData
{
    public string? Map { get; set; }
    public string? Data { get; set; }
    public string? MetaData { get; set; }
}