using BSL.v41.Logic.Database.Account;
using BSL.v41.Titan.DataStream;

namespace BSL.v41.Logic.Environment.LaserMessage.Sepo.Home.Laser.Laser;

public class BrawlPassSeasonData(AccountModel accountModel)
{
    public void Encode(ByteStream byteStream)
    {
        byteStream.WriteVInt(9);
        byteStream.WriteVInt(0);
        byteStream.WriteBoolean(false);
        byteStream.WriteVInt(0);

        byteStream.WriteByte(2);
        byteStream.WriteInt(0);
        byteStream.WriteInt(0);
        byteStream.WriteInt(0);
        byteStream.WriteInt(0);

        byteStream.WriteByte(1);
        byteStream.WriteInt(0);
        byteStream.WriteInt(0);
        byteStream.WriteInt(0);
        byteStream.WriteInt(0);
    }
}