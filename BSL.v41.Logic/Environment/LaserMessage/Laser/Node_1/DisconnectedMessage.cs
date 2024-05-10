using BSL.v41.Supercell.Titan.CommonUtils;
using BSL.v41.Titan.Message;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_1;

public class DisconnectedMessage : PiranhaMessage
{
    private int _reason;

    public DisconnectedMessage()
    {
        Helper.Skip();
    }

    public override void Encode()
    {
        base.Encode();

        ByteStream.WriteInt(_reason);
    }

    public override void Destruct()
    {
        base.Destruct();
    }

    public int GetReason()
    {
        return _reason;
    }

    public void SetReason(int reason)
    {
        _reason = reason;
    }

    public override int GetMessageType()
    {
        return TitanDisconnectedMessage.GetMessageType();
    }

    public override int GetServiceNodeType()
    {
        return TitanDisconnectedMessage.GetServiceNodeType();
    }
}