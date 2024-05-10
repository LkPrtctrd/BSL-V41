using BSL.v41.Logic.Environment.LaserMessage.Sepo.Home.Laser;
using BSL.v41.Logic.Environment.LaserMessage.Sepo.Mode;
using BSL.v41.Titan.DataStream;

namespace BSL.v41.Logic.Environment.LaserCommand.Laser;

public class LogicDayChangedCommand(LogicConfData logicConfData) : LogicCommand
{
    public override void Encode(ByteStream byteStream)
    {
        if (byteStream.WriteBoolean(true)) logicConfData.Encode(byteStream);
        new LogicServerCommand(byteStream).Encode();
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
        return 204;
    }
}