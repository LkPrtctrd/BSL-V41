namespace BSL.v41.Titan.Cloud;

public static class DynamicCloud
{
    private static int _printCount;
    private static int _warningCount;
    private static int _errorCount;

    public static void AddPrint()
    {
        _printCount++;
    }

    public static void AddWarning()
    {
        _warningCount++;
    }

    public static void AddError()
    {
        _errorCount++;
    }

    public static int GetPrintCount()
    {
        return _printCount;
    }

    public static int GetWarningCount()
    {
        return _warningCount;
    }

    public static int GetErrorCount()
    {
        return _errorCount;
    }
}