using BSL.v41.Titan.DataStream;

namespace BSL.v41.Logic.Environment.LaserMessage.Sepo.Home.Laser.Laser;

public class IntValueEntry(int x, int y)
{
    public int Key { get; set; } = x;
    public int Value { get; set; } = y;

    public void Encode(ByteStream byteStream)
    {
        byteStream.WriteInt(Key);
        byteStream.WriteInt(Value);
    }
}