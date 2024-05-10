namespace BSL.v41.Titan.Utilities.Sepo;

public class TimeHelper
{
    private long _date;

    public TimeHelper(long t)
    {
        _date = t;
    }

    public TimeHelper(long t, int days, int hours, int minutes, int seconds)
    {
        _date = t;
        {
            AddDays(days);
            AddHours(hours);
            AddMinutes(minutes);
            AddSeconds(seconds);
        }
    }

    public long AddDays(int days)
    {
        _date += (long)days * 60 * 60 * 24;
        return _date;
    }

    public long AddHours(int hours)
    {
        _date += (long)hours * 60 * 60;
        return _date;
    }

    public long AddMinutes(int minutes)
    {
        _date += (long)minutes * 60;
        return _date;
    }

    public long AddSeconds(int seconds)
    {
        _date += seconds;
        return _date;
    }

    public long GetNeedTime()
    {
        return _date;
    }
}