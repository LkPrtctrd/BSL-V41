using BSL.v41.Titan.DataStream;
using BSL.v41.Titan.DataStream.Helps;

namespace BSL.v41.Logic.Environment.LaserMessage.Sepo.Home.Laser.Laser;

public class VanityItemEntry(int vanityGlobalId)
{
    public void Encode(ByteStream byteStream)
    {
        ByteStreamHelper.WriteDataReference(byteStream, vanityGlobalId);
        byteStream.WriteVInt(0);
    }
}