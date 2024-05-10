using BSL.v41.Logic.Environment.LaserMessage.Sepo.Mode;
using BSL.v41.Titan.DataStream;

namespace BSL.v41.Logic.Environment.LaserCommand.Laser;

public class LogicChangeAvatarNameCommand(string newName, int changeNameCost) : LogicCommand
{
    public override void Encode(ByteStream byteStream)
    {
        byteStream.WriteString(newName);
        byteStream.WriteVInt(changeNameCost);

        new LogicServerCommand(byteStream).Encode();
    }

    public override void Decode(ByteStream byteStream)
    {
        byteStream.ReadString(9999);
        byteStream.ReadVInt();

        new LogicServerCommand(byteStream).Decode();
    }

    public override bool CanExecute(LogicHomeMode logicHomeMode)
    {
        return false;
    }

    public override int Execute(LogicHomeMode logicHomeMode)
    {
        return 0;
    }

    public override int GetCommandType()
    {
        return 201;
    }
}