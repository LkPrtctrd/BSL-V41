using BSL.v41.Supercell.Titan.CommonUtils.Utils;
using BSL.v41.Titan.DataStream;
using BSL.v41.Titan.DataStream.Helps;
using BSL.v41.Titan.Mathematical.Data;

namespace BSL.v41.Logic.Environment.LaserMessage.Sepo.Player;

public class LogicBattleEmotes
{
    public void Encode(ByteStream byteStream)
    {
        byteStream.WriteVInt(5);
        {
            ByteStreamHelper.WriteDataReference(byteStream,
                GlobalId.CreateGlobalId(CsvHelperTable.Emotes.GetId(), 178 - 3));
            ByteStreamHelper.WriteDataReference(byteStream,
                GlobalId.CreateGlobalId(CsvHelperTable.Emotes.GetId(), 148 - 3));
            ByteStreamHelper.WriteDataReference(byteStream,
                GlobalId.CreateGlobalId(CsvHelperTable.Emotes.GetId(), 345 - 3));
            ByteStreamHelper.WriteDataReference(byteStream,
                GlobalId.CreateGlobalId(CsvHelperTable.Emotes.GetId(), 137 - 3));
            ByteStreamHelper.WriteDataReference(byteStream,
                GlobalId.CreateGlobalId(CsvHelperTable.Emotes.GetId(), 67 - 3));
        }
    }
}