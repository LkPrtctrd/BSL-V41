using BSL.v41.Logic.Environment.LaserCommand;
using BSL.v41.Supercell.Titan.CommonUtils;
using BSL.v41.Titan.Graphic;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_9;

public class AvailableServerCommandMessage : PiranhaMessage
{
    private LogicCommand _logicCommand = null!;

    public AvailableServerCommandMessage()
    {
        Helper.Skip();
    }

    public override void Encode()
    {
        base.Encode();

        LogicCommandManager.EncodeCommand(_logicCommand, ByteStream);
    }

    public void SetServerCommand(LogicCommand logicCommand)
    {
        _logicCommand = logicCommand;
        ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Cmd,
            $"Set command received! {logicCommand.GetType().Name}.");
    }

    public override int GetMessageType()
    {
        return 24111;
    }

    public override int GetServiceNodeType()
    {
        return 9;
    }
}