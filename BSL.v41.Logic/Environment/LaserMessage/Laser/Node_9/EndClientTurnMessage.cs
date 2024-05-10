using BSL.v41.Logic.Environment.LaserCommand;
using BSL.v41.Supercell.Titan.CommonUtils;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_9;

public class EndClientTurnMessage : PiranhaMessage
{
    private readonly List<LogicCommand> _logicCommands = [];
    private int _checksum;
    private int _tick;

    public EndClientTurnMessage()
    {
        Helper.Skip();
    }

    public override void Decode()
    {
        base.Decode();

        ByteStream.ReadBoolean();
        _tick = ByteStream.ReadVInt();
        _checksum = ByteStream.ReadVInt();
        var commands = ByteStream.ReadVInt();

        for (var i = 0; i < commands; i++)
        {
            var command = LogicCommandManager.DecodeCommand(ByteStream);
            {
                if (command != null) _logicCommands.Add(command);
            }
        }
    }

    public override void Clear()
    {
        base.Clear();
    }

    public int GetTick()
    {
        return _tick;
    }

    public int GetChecksum()
    {
        return _checksum;
    }

    public IEnumerable<LogicCommand> GetCommands()
    {
        return _logicCommands;
    }

    public override int GetMessageType()
    {
        return 14102;
    }

    public override int GetServiceNodeType()
    {
        return 9;
    }
}