using BSL.v41.Supercell.Titan.CommonUtils;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_9;

public class MatchmakeFailedMessage : PiranhaMessage
{
    private int _errorCode;

    public MatchmakeFailedMessage()
    {
        Helper.Skip();
    }

    public override void Encode()
    {
        base.Encode();

        ByteStream.WriteInt(_errorCode);
    }

    public override void Destruct()
    {
        base.Destruct();
    }

    public int GetErrorCode()
    {
        return _errorCode;
    }

    public void SetErrorCode(int errorCode)
    {
        _errorCode = errorCode;
    }

    public override int GetMessageType()
    {
        return 24108;
    }

    public override int GetServiceNodeType()
    {
        return 9;
    }
}