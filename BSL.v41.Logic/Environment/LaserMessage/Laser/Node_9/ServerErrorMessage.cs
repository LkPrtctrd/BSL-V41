using BSL.v41.Supercell.Titan.CommonUtils;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_9;

public class ServerErrorMessage(int reason) : PiranhaMessage
{
    public ServerErrorMessage() : this(100)
    {
        Helper.Skip();
    }

    public override void Encode()
    {
        base.Encode();

        ByteStream.WriteInt(reason);
    }

    public override void Destruct()
    {
        base.Destruct();
    }

    public override int GetMessageType()
    {
        return 24115;
    }

    public override int GetServiceNodeType()
    {
        return 9;
    }
}