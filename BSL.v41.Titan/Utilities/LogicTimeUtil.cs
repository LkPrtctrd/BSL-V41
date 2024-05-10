namespace BSL.v41.Titan.Utilities;

public static class LogicTimeUtil
{
    private static readonly DateTime UnixTime = new(1970, 1, 1);

    public static int GetTimestamp()
    {
        return (int)DateTime.UtcNow.Subtract(UnixTime).TotalSeconds;
    }

    public static int GetTimestamp(DateTime time)
    {
        return (int)time.Subtract(UnixTime).TotalSeconds;
    }

    public static string GetTimestampMs()
    {
        return DateTime.UtcNow.Subtract(UnixTime).TotalMilliseconds.ToString("#");
    }

    public static DateTime GetDateTimeFromTimestamp(int timestamp)
    {
        return UnixTime.AddSeconds(timestamp);
    }

    public static int GetDayIndex()
    {
        return DateTime.UtcNow.Year * 1000 + DateTime.UtcNow.DayOfYear;
    }

    public static int GetTimeOfDay()
    {
        return DateTime.UtcNow.Hour * 3600 + DateTime.UtcNow.Minute * 60 + DateTime.UtcNow.Second;
    }

    public static int GetCurrentTimeInSecondsSinceEpoch()
    {
        return (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}