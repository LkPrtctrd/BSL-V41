namespace BSL.v41.Titan.Utilities;

public class LogicStringUtil
{
    public static string IntToString(int value)
    {
        return IntToStringWithThousandSeparator(value);
    }

    public static string IntToStringWithThousandSeparator(object value)
    {
        return string.Concat(value.ToString(), "");
    }
}