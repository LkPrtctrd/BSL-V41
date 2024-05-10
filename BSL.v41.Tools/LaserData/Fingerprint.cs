using Newtonsoft.Json;

namespace BSL.v41.Tools.LaserData;

public static class Fingerprint
{
    public static string? Patch { get; set; }
    public static dynamic? Data { get; set; }

    public static void Parse()
    {
        Data = JsonConvert.DeserializeObject(File.ReadAllText(Patch!)) ?? throw new InvalidOperationException();
    }

    public static int GetMajorVersion()
    {
        return int.Parse((Data!.version.ToString().Split('.')[0] as string)!);
    }

    public static int GetMinorVersion()
    {
        return int.Parse(Data!.version.ToString().Split('.')[1]);
    }

    public static int GetBuildVersion()
    {
        return int.Parse(Data!.version.ToString().Split('.')[2]);
    }
}