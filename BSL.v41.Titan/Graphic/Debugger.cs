using BSL.v41.Titan.Cloud;

namespace BSL.v41.Titan.Graphic;

public static class Debugger
{
    private static int _printCount;
    private static int _warningCount;
    private static int _errorCount;

    public static void Print(string log)
    {
        DynamicCloud.AddPrint();
        {
            _printCount++;
        }

        ConsoleLogger.Print($"/{_printCount} [print] " + log);
    }

    public static void Warning(string log)
    {
        DynamicCloud.AddWarning();
        {
            _warningCount++;
        }

        ConsoleLogger.Print($"/{_warningCount} [warn] " + log);
    }

    public static void Error(string log)
    {
        DynamicCloud.AddError();
        {
            _errorCount++;
        }

        ConsoleLogger.Print($"/{_errorCount} [error] " + log);
    }
}