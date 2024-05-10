using BSL.v41.Titan.DataStream;
using BSL.v41.Titan.DataStream.Helps;
using BSL.v41.Titan.Utilities;

namespace BSL.v41.Logic.Environment.LaserMessage.Sepo.Event;

public class EventData(int endTime, int slot, int location, int rewardCount, bool upcomingEvent = false)
{
    public void Encode(ByteStream byteStream)
    {
        byteStream.WriteVInt(!upcomingEvent ? 2 : 1);
        byteStream.WriteVInt(slot);
        byteStream.WriteVInt(upcomingEvent ? endTime - LogicTimeUtil.GetTimestamp() : 0);
        byteStream.WriteVInt(!upcomingEvent ? endTime - LogicTimeUtil.GetTimestamp() : endTime - 0);
        byteStream.WriteVInt(0);
        ByteStreamHelper.WriteDataReference(byteStream, location < 1000000 ? 15 * 1000000 + location : location);
        byteStream.WriteVInt(2);
        byteStream.WriteVInt(0);
        byteStream.WriteString("");
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0); // Array (Modifiers)
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteBoolean(false);
        byteStream.WriteVInt(0);
        byteStream.WriteBoolean(false);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteBoolean(false);
        byteStream.WriteBoolean(false);
        byteStream.WriteBoolean(false);
        byteStream.WriteVInt(0);
        byteStream.WriteBoolean(false);
        byteStream.WriteBoolean(false);
    }

    public void Encode(Custom.Stream.Proxy.ByteStream byteStream)
    {
        byteStream.WriteVInt(!upcomingEvent ? 2 : 1);
        byteStream.WriteVInt(slot);
        byteStream.WriteVInt(upcomingEvent ? endTime - LogicTimeUtil.GetTimestamp() : 0);
        byteStream.WriteVInt(!upcomingEvent ? endTime - LogicTimeUtil.GetTimestamp() : endTime - 0);
        byteStream.WriteVInt(0);
        byteStream.WriteDataReference(location < 1000000 ? 15 * 1000000 + location : location);
        byteStream.WriteVInt(2);
        byteStream.WriteVInt(0);
        byteStream.WriteString("");
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
    }

    public int GetLocation()
    {
        return location < 1000000 ? 15 * 1000000 + location : location;
    }

    public int GetSlot()
    {
        return slot;
    }

    public int GetEndTime()
    {
        return endTime;
    }

    public bool GetTimeFinished()
    {
        return endTime - LogicTimeUtil.GetTimestamp() <= 1;
    }
}