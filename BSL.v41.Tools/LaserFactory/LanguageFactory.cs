using Newtonsoft.Json.Linq;

namespace BSL.v41.Tools.LaserFactory;

public enum LanguageFactory
{
    English,
    Russian,
    Chinese,
    German
}

public static class LanguageFactoryExtensions
{
    public static string GetTextCodeByLanguage(this LanguageFactory languageFactory)
    {
        return languageFactory switch
        {
            LanguageFactory.English => "en",
            LanguageFactory.Russian => "ru",
            LanguageFactory.Chinese => "zh-CN",
            LanguageFactory.German => "de",
            _ => "en"
        };
    }

    public static string TranslateTo(this LanguageFactory languageFactory, string text)
    {
        var apiUrl = $"https://translate.googleapis.com/translate_a/single?" +
                     $"client=gtx&sl=auto&tl={GetTextCodeByLanguage(languageFactory)}&dt=t&q={Uri.EscapeDataString(text)}";
        return ((JArray)JArray.Parse(new HttpClient().GetAsync(apiUrl).Result.Content.ReadAsStringAsync().Result)[0])[0]
            [0]!.ToString();
    }
}