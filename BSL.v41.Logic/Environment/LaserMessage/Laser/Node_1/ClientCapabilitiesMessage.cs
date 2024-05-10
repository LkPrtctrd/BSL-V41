using BSL.v41.Supercell.Titan.CommonUtils;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_1;

public class ClientCapabilitiesMessage : PiranhaMessage
{
    private int _ping;

    public ClientCapabilitiesMessage()
    {
        Helper.Skip();
    }

    public override void Decode()
    {
        base.Decode();

        _ping = ByteStream.ReadVInt();
    }

    public override void Clear()
    {
        base.Clear();
    }

    public int GetPing()
    {
        return _ping;
    }

    public void SetPing(int ping)
    {
        _ping = ping;
    }

    public override int GetMessageType()
    {
        return 10107;
    }

    public override int GetServiceNodeType()
    {
        return 1;
    }
}