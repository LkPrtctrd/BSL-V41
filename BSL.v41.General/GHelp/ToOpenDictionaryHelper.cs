using Newtonsoft.Json;
using BSL.v41.Logic.Environment.LaserMessage.Sepo.Alliance;
using BSL.v41.Supercell.Titan.CommonUtils.HelpMap;

namespace BSL.v41.General.GHelp;

public static class ToOpenDictionaryHelper
{
    public static Dictionary<long, AllianceMemberEntry?> ToAllianceMemberEntryDictionary(object obj)
    {
        var settings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> { new LongDictionaryKeysConverter() }
        };

        return JsonConvert.DeserializeObject<Dictionary<long, AllianceMemberEntry?>>(
            JsonConvert.SerializeObject(obj, settings))!;
    }
}