using BSL.v41.Titan.DataStream;
using BSL.v41.Titan.DataStream.Helps;

namespace BSL.v41.Logic.Environment.LaserMessage.Sepo.Home.Laser.Laser;

public class CooldownEntry
{
    public void Encode(ByteStream byteStream)
    {
        byteStream.WriteVInt(0);
        ByteStreamHelper.WriteDataReference(byteStream, 0);
        byteStream.WriteVInt(0);
    }
}