using BSL.v41.Titan.DataStream;
using BSL.v41.Titan.DataStream.Helps;
using BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

namespace BSL.v41.Logic.Environment.LaserMessage.Sepo.Player;

public class LogicHeroUpgrades
{
    public void Encode(ByteStream byteStream, List<LogicCardData> cardInfo)
    {
        byteStream.WriteVInt(0);
        ByteStreamHelper.WriteDataReference(byteStream, cardInfo[0].GetGlobalId());
        ByteStreamHelper.WriteDataReference(byteStream, cardInfo[1].GetGlobalId());
        ByteStreamHelper.WriteDataReference(byteStream, 0);
        ByteStreamHelper.WriteDataReference(byteStream, 0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
    }
}