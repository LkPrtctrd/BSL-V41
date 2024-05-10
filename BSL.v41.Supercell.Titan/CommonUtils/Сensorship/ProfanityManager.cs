namespace BSL.v41.Supercell.Titan.CommonUtils.Сensorship;

public static class ProfanityManager
{
    private static string? _path;
    private static string[]? _fileLines;

    public static void Initialize(string? path)
    {
        _path = path;
        _fileLines = File.ReadAllLines(_path!);
    }

    public static string ProfanitySerialize(string deltaText)
    {
        return _fileLines!.Any(value => deltaText.Contains(value, StringComparison.CurrentCultureIgnoreCase))
            ? new string('*', deltaText.Length)
            : deltaText;
    }

    public static bool ProfanityContainCheck(string deltaText)
    {
        return _fileLines!.Any(value => deltaText.Contains(value, StringComparison.CurrentCultureIgnoreCase));
    }
}