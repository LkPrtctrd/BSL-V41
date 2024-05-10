using BSL.v41.Titan.DataStream;

namespace BSL.v41.Logic.Environment.LaserMessage.Sepo.Event;

public class EventSlot(int counter)
{
    public void Encode(ByteStream byteStream)
    {
        byteStream.WriteVInt(counter);
    }

    public void Encode(Custom.Stream.Proxy.ByteStream byteStream)
    {
        byteStream.WriteVInt(counter);
    }
}