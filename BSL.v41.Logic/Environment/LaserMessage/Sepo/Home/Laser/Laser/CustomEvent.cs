using BSL.v41.Titan.DataStream;

namespace BSL.v41.Logic.Environment.LaserMessage.Sepo.Home.Laser.Laser;

public class CustomEvent
{
    public void Encode(ByteStream byteStream)
    {
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);

        new ChronosTextEntry("").Encode(byteStream);
        new ChronosTextEntry("").Encode(byteStream);
        new ChronosTextEntry("").Encode(byteStream);
    }
}