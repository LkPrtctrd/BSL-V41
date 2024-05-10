using BSL.v41.Titan.DataStream;
using BSL.v41.Titan.DataStream.Helps;

namespace BSL.v41.Logic.Environment.LaserMessage.Sepo.Home.Laser.Laser;

public class ReleaseEntry
{
    public void Encode(ByteStream byteStream)
    {
        ByteStreamHelper.WriteDataReference(byteStream, 0);
        byteStream.WriteInt(0);
        byteStream.WriteInt(0);
    }
}