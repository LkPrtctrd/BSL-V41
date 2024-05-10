using System.Globalization;

namespace BSL.v41.Titan.Utilities.Sepo;

public static class TimeUtil
{
    private const int TicksInSecond = 20;

    public static string GetTicksAsSecondsString(int ticks)
    {
        return (ticks / TicksInSecond).ToString();
    }

    public static string GetClockStringWithZeroes()
    {
        return DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds
            .ToString(CultureInfo.InvariantCulture);
    }
}